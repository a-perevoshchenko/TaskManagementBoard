using System.ComponentModel.DataAnnotations;

namespace Task.Management.Board.Models;

public class Project
{
    [Key]
    public int Id { get; set; }

    [Required] 
    [StringLength(30, ErrorMessage = "Name cannot has more than 30 symbols")]
    public string Name { get; set; } = "";
    
    [StringLength(500, ErrorMessage = "Description cannot has more than 500 symbols")]
    public string? Description { get; set; }
    
    public ICollection<Issue> Issues { get; set; } = new List<Issue>();
    public ICollection<ApplicationUser> Members { get; set; } = new List<ApplicationUser>();
}