using System.ComponentModel.DataAnnotations;

namespace Task.Management.Board.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(300, ErrorMessage = "Comment cannot has more than 300 symbols")]
    public string Content { get; set; } = "";
    
    [Required]
    public int IssueId { get; set; }
    public Issue? Issue { get; set; }
    
    [Required]
    public string UserId { get; set; } = "";
    public ApplicationUser? User { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}