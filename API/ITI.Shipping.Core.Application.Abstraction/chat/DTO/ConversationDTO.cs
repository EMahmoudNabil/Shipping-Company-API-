// APIs/DTOs/ConversationDTOs.cs
using System.ComponentModel.DataAnnotations;

public class ConversationDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ConversationStatus Status { get; set; }
    public ICollection<ChatMessageDTO> Messages { get; set; }
}

public class CreateConversationDTO
{
    [Required]
    public string UserId { get; set; }
}