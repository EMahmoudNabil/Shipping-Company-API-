// Core/Domain/Entities/Chatbot/Conversation.cs
public class Conversation
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ConversationStatus Status { get; set; }
    public virtual ICollection<ChatMessage> Messages { get; set; } // Mark as virtual
    public DateTime? LastMessageDate { get; set; } // أضف هذه الخاصية
}

public enum ConversationStatus
{
    Active,
    Closed,
    WaitingForResponse
}