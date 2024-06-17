using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Task.Management.Board.Models;

public class ApplicationUser: IdentityUser
{
    [StringLength(50, ErrorMessage = "Full Name cannot has more than 50 symbols")]
    public string? FullName { get; set; }
    
    public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
    public ICollection<Project>? Projects { get; set; } = new List<Project>();
}