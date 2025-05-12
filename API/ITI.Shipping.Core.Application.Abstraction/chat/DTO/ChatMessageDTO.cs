using System.ComponentModel.DataAnnotations;

public class ChatMessageDTO
{
    public int Id { get; set; }
    public int ConversationId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public MessageSender Sender { get; set; }
    public MessageType Type { get; set; }
    public List<QuickReplyDTO> QuickReplies { get; set; }
}

public class CreateChatMessageDTO
{
    [Required]
    public int ConversationId { get; set; }

    [Required]
    public string Content { get; set; }

    public MessageSender Sender { get; set; } = MessageSender.User;
    public MessageType Type { get; set; } = MessageType.Text;
}

public class QuickReplyDTO
{
    public string Title { get; set; }
    public string Payload { get; set; }
}