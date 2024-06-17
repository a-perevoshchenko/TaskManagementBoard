namespace Task.Management.Board.Models;

public class EditUserRolesViewModel
{
    public string UserId { get; set; } = "";
    public string Email { get; set; } = "";
    public string? FullName { get; set; } = "";
    public List<string> Roles { get; set; } = [];
    public List<string?> AllRoles { get; set; } = [];
    public List<string> SelectedRoles { get; set; } = [];
}