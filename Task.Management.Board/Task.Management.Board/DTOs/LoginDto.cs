using System.ComponentModel.DataAnnotations;

namespace Task.Management.Board.DTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = "";
}