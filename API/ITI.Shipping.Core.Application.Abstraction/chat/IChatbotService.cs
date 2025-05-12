// Services/Interfaces/IChatbotService.cs
public interface IChatbotService
{
    Task<Conversation> StartNewConversation(string userId);
    Task<ChatMessage> AddUserMessage(int conversationId, string message);
    Task<IEnumerable<ChatMessage>> GetConversationMessages(int conversationId);
    Task<Conversation> GetConversation(int conversationId);
    Task CloseConversation(int conversationId);
    Task<bool> ValidateConversationOwnership(int conversationId, string userId);
    Task<Conversation> StartConversation(string? userId);
    Task CleanupInactiveConversations();
}
