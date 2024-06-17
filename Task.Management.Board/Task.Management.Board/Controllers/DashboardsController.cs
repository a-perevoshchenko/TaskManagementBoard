using Microsoft.AspNetCore.Mvc;
using Task.Management.Board.Data;
using Task.Management.Board.Models;
using Task.Management.Board.Models.Enums;

namespace Task.Management.Board.Controllers;

public class DashboardsController(ApplicationDbContext context) : Controller
{
    // GET: Dashboard
    public IActionResult Index()
    {
        var projectCount = context.Projects.Count();
        var issueCount = context.Issues.Count();
        var openIssuesCount = context.Issues.Count(i => i.Status != IssueStatus.Done);
        var closedIssuesCount = context.Issues.Count(i => i.Status == IssueStatus.Done);

        var model = new DashboardViewModel
        {
            ProjectCount = projectCount,
            IssueCount = issueCount,
            OpenIssuesCount = openIssuesCount,
            ClosedIssuesCount = closedIssuesCount
        };

        return View(model);
    }
}