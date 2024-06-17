using System.ComponentModel.DataAnnotations;

namespace Task.Management.Board.DTOs;

public class CommentDto
{
    public int Id { get; set; }
    
    [Required]
    public int IssueId { get; set; }

    [Required] 
    public string UserId { get; set; } = "";

    [Required]
    [StringLength(300, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
    public string Content { get; set; } = "";
}