// Core/Domain/Entities/Chatbot/ChatMessage.cs
// ChatMessage class
public class ChatMessage
{
    public int Id { get; set; }
    public int ConversationId { get; set; }
    public virtual Conversation Conversation { get; set; } // Mark as virtual
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public MessageSender Sender { get; set; }
    public MessageType Type { get; set; }
}

public enum MessageSender
{
    User,
    Bot,
    System
}

public enum MessageType
{
    Text,
    QuickReply,
    ShipmentDetails,
    TrackingUpdate,
    Image,
    Document
}