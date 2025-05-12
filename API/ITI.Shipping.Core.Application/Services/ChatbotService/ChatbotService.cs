// Services/ChatbotService.cs
using ITI.Shipping.Core.Application.Abstraction.chat;
using ITI.Shipping.Core.Application.Abstraction.User;
using ITI.Shipping.Core.Application.Services.ChatbotService;
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class ChatbotService : IChatbotService
{

    private readonly ApplicationContext _context;
    private readonly IUserService _userService;
    private readonly IShipmentService _shipmentService;
    private readonly IAIService _aiService;
    private readonly ILogger<ChatbotService> _logger;

    public ChatbotService(
        ApplicationContext context,
        IUserService userService,
        IShipmentService shipmentService,
        IAIService aiService,
        ILogger<ChatbotService> logger)
    {
        _context = context;
        _userService = userService;
        _shipmentService = shipmentService;
        _aiService = aiService;
        _logger = logger;
    }

    public async Task<Conversation> StartConversation(string? userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("User ID cannot be null or empty");

        var conversation = await _context.Conversations
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == ConversationStatus.Active);

        if (conversation != null)
        {
            await AddSystemMessage(conversation.Id, "مرحباً بك مرة أخرى، كيف يمكنني مساعدتك؟");
            return conversation;
        }
        else
        {
            return await StartNewConversation(userId);
        }
    }

    public async Task<Conversation> StartNewConversation(string userId)
    {
        try
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            var conversation = new Conversation
            {
                UserId = userId,
                Status = ConversationStatus.Active,
                StartDate = DateTime.UtcNow
            };

            await _context.Conversations.AddAsync(conversation);
            await _context.SaveChangesAsync();

            await AddSystemMessage(conversation.Id, "مرحباً بك في خدمة الدعم الفني، كيف يمكنني مساعدتك؟");
            return conversation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting new conversation");
            throw;
        }
    }

    public async Task CleanupInactiveConversations()
    {
        var cutoff = DateTime.UtcNow.AddHours(-24);
        var conversations = await _context.Conversations
            .Where(c => c.Status == ConversationStatus.Active &&
                       c.LastMessageDate < cutoff)
            .ToListAsync();

        foreach (var conv in conversations)
        {
            conv.Status = ConversationStatus.Closed;
            conv.EndDate = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }
    public async Task<ChatMessage> AddUserMessage(int conversationId, string content)
    {
        var userMessage = new ChatMessage
        {
            ConversationId = conversationId,
            Content = content,
            Timestamp = DateTime.UtcNow,
            Sender = MessageSender.User,
            Type = MessageType.Text
        };

        await _context.ChatMessages.AddAsync(userMessage);

        // تحديث LastMessageDate في المحادثة
        var conversation = await _context.Conversations.FindAsync(conversationId);
        if (conversation != null)
        {
            conversation.LastMessageDate = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        var aiResponse = await _aiService.ProcessUserMessageAsync(content, conversation.UserId);

        var botMessage = new ChatMessage
        {
            ConversationId = conversationId,
            Content = aiResponse.Response,
            Timestamp = DateTime.UtcNow,
            Sender = MessageSender.Bot,
            Type = aiResponse.ResponseType
        };

        await _context.ChatMessages.AddAsync(botMessage);
        await _context.SaveChangesAsync();

        return userMessage;
    }


    public async Task<IEnumerable<ChatMessage>> GetConversationMessages(int conversationId)
    {
        return await _context.ChatMessages
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.Timestamp)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Conversation> GetConversation(int conversationId)
    {
        return await _context.Conversations
            .Include(c => c.Messages)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == conversationId);
    }

    public async Task CloseConversation(int conversationId)
    {
        var conversation = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Id == conversationId);
            
        if (conversation != null)
        {
            conversation.Status = ConversationStatus.Closed;
            conversation.EndDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ValidateConversationOwnership(int conversationId, string userId)
    {
        return await _context.Conversations
            .AnyAsync(c => c.Id == conversationId && c.UserId == userId);
    }

    private async Task AddSystemMessage(int conversationId, string content)
    {
        var message = new ChatMessage
        {
            ConversationId = conversationId,
            Content = content,
            Sender = MessageSender.System,
            Type = MessageType.Text,
            Timestamp = DateTime.UtcNow
        };

        await _context.ChatMessages.AddAsync(message);
        await _context.SaveChangesAsync();
    }

    private async Task ProcessUserMessage(int conversationId, string message)
    {
        if (ContainsTrackingKeywords(message))
        {
            await HandleTrackingRequest(conversationId, message);
        }
        else if (ContainsComplaintKeywords(message))
        {
            await HandleComplaint(conversationId);
        }
        else
        {
            await AddBotMessage(conversationId, "هل تقصد تتبع شحنة أو تقديم استفسار آخر؟");
        }
    }

    private bool ContainsTrackingKeywords(string message)
    {
        var keywords = new[] { "تتبع", "تتبع شحنة", "track", "tracking" };
        return keywords.Any(k => message.Contains(k, StringComparison.OrdinalIgnoreCase));
    }

    private bool ContainsComplaintKeywords(string message)
    {
        var keywords = new[] { "شكوى", "مشكلة", "complaint", "problem" };
        return keywords.Any(k => message.Contains(k, StringComparison.OrdinalIgnoreCase));
    }

    private async Task HandleTrackingRequest(int conversationId, string message)
    {
        var trackingNumber = ExtractTrackingNumber(message);
        
        if (string.IsNullOrEmpty(trackingNumber))
        {
            await AddBotMessage(conversationId, "الرجاء إدخال رقم تتبع الشحنة");
            return;
        }

        try
        {
            var shipment = await _shipmentService.GetShipmentByTrackingNumber(trackingNumber);
            
            if (shipment == null)
            {
                await AddBotMessage(conversationId, "عفواً، لم يتم العثور على شحنة بهذا الرقم");
                return;
            }

            var statusMessage = BuildShipmentStatusMessage(shipment);
            await AddBotMessage(conversationId, statusMessage, MessageType.ShipmentDetails);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling tracking request");
            await AddBotMessage(conversationId, "حدث خطأ أثناء معالجة طلبك. يرجى المحاولة لاحقاً");
        }
    }

    private string BuildShipmentStatusMessage(Shipment shipment)
    {
        return $"معلومات الشحنة:\n" +
               $"رقم التتبع: {shipment.TrackingNumber}\n" +
               $"الحالة: {GetArabicStatus(shipment.Status)}\n" +
               $"الموقع الحالي: {shipment.CurrentLocation}\n" +
               $"التاريخ المتوقع للتسليم: {shipment.EstimatedDeliveryDate:yyyy-MM-dd}";
    }

    private string GetArabicStatus(ShipmentStatus status)
    {
        return status switch
        {
            ShipmentStatus.Pending => "قيد الانتظار",
            ShipmentStatus.InTransit => "في الطريق",
            ShipmentStatus.OutForDelivery => "خارج للتوصيل",
            ShipmentStatus.Delivered => "تم التسليم",
            ShipmentStatus.Delayed => "متأخر",
            _ => "غير معروف"
        };
    }

    private async Task HandleComplaint(int conversationId)
    {
        await AddBotMessage(conversationId, "نعتذر للإزعاج. سيقوم ممثل خدمة العملاء بالتواصل معك في أقرب وقت.");
        await AddBotMessage(conversationId, "هل هناك أي شيء آخر يمكنني مساعدتك به؟");
    }

    private async Task AddBotMessage(int conversationId, string content, MessageType type = MessageType.Text)
    {
        var chatMessage = new ChatMessage
        {
            ConversationId = conversationId,
            Content = content,
            Timestamp = DateTime.UtcNow,
            Sender = MessageSender.Bot,
            Type = type
        };

        await _context.ChatMessages.AddAsync(chatMessage);
        await _context.SaveChangesAsync();
    }

    private string ExtractTrackingNumber(string message)
    {
        // Improved extraction with regex
        var pattern = @"\b[A-Z0-9]{8,}\b";
        var match = System.Text.RegularExpressions.Regex.Match(message, pattern);
        return match.Success ? match.Value : null;
    }
}