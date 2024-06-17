namespace Task.Management.Board.Models;

public class UserRolesViewModel
{
    public string UserId { get; set; } = "";
    public string? Email { get; set; } = "";
    public string? FullName { get; set; } = "";
    public List<string> Roles { get; set; } = [];
}