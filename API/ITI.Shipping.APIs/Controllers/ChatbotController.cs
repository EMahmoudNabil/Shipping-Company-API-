// APIs/Controllers/ChatbotController.cs
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

using ITI.Shipping.Core.Application.Abstraction.User;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatbotController : ControllerBase
{
    private readonly IChatbotService _chatbotService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public ChatbotController(
        IChatbotService chatbotService,
        IUserService userService,
        IMapper mapper)
    {
        _chatbotService = chatbotService;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartConversation()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var conversation = await _chatbotService.StartConversation(userId);
        var conversationDto = _mapper.Map<ConversationDTO>(conversation);
        return Ok(conversationDto);
    }

 
    [HttpPost("{conversationId}/messages")]
    public async Task<IActionResult> SendMessage(int conversationId, [FromBody] CreateChatMessageDTO request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var isValid = await _chatbotService.ValidateConversationOwnership(conversationId, userId);
        if (!isValid) return Forbid();

        var message = _mapper.Map<ChatMessage>(request);
        var createdMessage = await _chatbotService.AddUserMessage(conversationId, message.Content);

        // الحصول على آخر رسائل المحادثة (التي تتضمن الآن رد الذكاء الاصطناعي)
        var messages = await _chatbotService.GetConversationMessages(conversationId);
        var lastTwoMessages = messages.OrderByDescending(m => m.Timestamp).Take(2).OrderBy(m => m.Timestamp);

        return Ok(_mapper.Map<IEnumerable<ChatMessageDTO>>(lastTwoMessages));
    }

    [HttpGet("{conversationId}/messages")]
    public async Task<IActionResult> GetMessages(int conversationId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var isValid = await _chatbotService.ValidateConversationOwnership(conversationId, userId);
        if (!isValid) return Forbid();

        var messages = await _chatbotService.GetConversationMessages(conversationId);
        var messagesDto = _mapper.Map<IEnumerable<ChatMessageDTO>>(messages);

        return Ok(messagesDto);
    }

    [HttpDelete("{conversationId}")]
    public async Task<IActionResult> CloseConversation(int conversationId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isValid = await _chatbotService.ValidateConversationOwnership(conversationId, userId);
        if (!isValid) return Forbid();

        await _chatbotService.CloseConversation(conversationId);
        return Ok("Conversation closed successfully.");
    }
}