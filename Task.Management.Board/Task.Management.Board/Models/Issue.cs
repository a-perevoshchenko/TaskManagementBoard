using System.ComponentModel.DataAnnotations;
using Task.Management.Board.Models.Enums;

namespace Task.Management.Board.Models;

public class Issue
{
    [Key]
    public int Id { get; set; }

    [Required] 
    [StringLength(100, ErrorMessage = "Title cannot has more than 100 symbols")]
    public string Title { get; set; } = "";
    
    [Required] 
    [StringLength(1000, ErrorMessage = "Comment cannot has more than 1000 symbols")]
    public string Description { get; set; } = "";
    
    [Required]
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    
    public string? AssignedToUserId { get; set; }
    public ApplicationUser? AssignedToUser { get; set; }
    
    public IssuePriority Priority { get; set; }
    public IssueStatus Status { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}