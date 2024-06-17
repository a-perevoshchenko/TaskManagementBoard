using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task.Management.Board.Data;
using Task.Management.Board.Models;

namespace Task.Management.Board.Controllers;

public class IssuesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    : Controller
{
    // GET: Issues/Index
    public IActionResult Index()
    {
        var issues = context.Issues.ToList();
        
        return View(issues);
    }

    // GET: Issues/Create
    public IActionResult Create()
    {
        ViewBag.Projects = context.Projects.ToList();
        ViewBag.Users = userManager.Users.ToList();
        
        return View();
    }

    // POST: Issues/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Issue model)
    {
        if (ModelState.IsValid)
        {
            context.Issues.Add(model);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        ViewBag.Projects = context.Projects.ToList();
        ViewBag.Users = userManager.Users.ToList();
        
        return View(model);
    }

    // GET: Issues/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0) return NotFound();

        var issue = await context.Issues.FindAsync(id);
        
        if (issue == null) return NotFound();
        
        ViewBag.Projects = context.Projects.ToList();
        ViewBag.Users = userManager.Users.ToList();
        
        return View(issue);
    }

    // POST: Issues/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Issue model)
    {
        if (id != model.Id) return NotFound();

        if (ModelState.IsValid)
        {
            context.Update(model);
            await context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        
        ViewBag.Projects = context.Projects.ToList();
        ViewBag.Users = userManager.Users.ToList();
        
        return View(model);
    }

    // GET: Issues/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return NotFound();

        var issue = await context.Issues.FindAsync(id);
        
        if (issue == null) return NotFound();

        return View(issue);
    }

    // POST: Issues/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var issue = await context.Issues.FindAsync(id);
        
        if (issue != null) context.Issues.Remove(issue);
        await context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));
    }

    // GET: Issues/Details/5
    public async Task<IActionResult> Details(int id)
    {
        if (id == 0) return NotFound();
        
        var issue = await context.Issues.FindAsync(id);
        
        if (issue == null) return NotFound();

        return View(issue);
    }
}