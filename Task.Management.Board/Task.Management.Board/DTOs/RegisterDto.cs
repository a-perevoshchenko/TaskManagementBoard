using System.ComponentModel.DataAnnotations;

namespace Task.Management.Board.DTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = "";

    [Required]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = "";
}