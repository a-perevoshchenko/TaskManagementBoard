namespace Task.Management.Board.Models;

public class DashboardViewModel
{
    public int ProjectCount { get; set; }
    public int IssueCount { get; set; }
    public int OpenIssuesCount { get; set; }
    public int ClosedIssuesCount { get; set; }
}