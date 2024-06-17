using Microsoft.AspNetCore.Mvc;
using Task.Management.Board.Data;
using Task.Management.Board.Models;

namespace Task.Management.Board.Controllers;

public class ProjectsController(ApplicationDbContext context) : Controller
{
    // GET: Projects/Index
    public IActionResult Index()
    {
        var projects = context.Projects.ToList();
        return View(projects);
    }

    // GET: Projects/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Projects/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Project model)
    {
        if (!ModelState.IsValid) return View(model);
        
        context.Projects.Add(model);
        await context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));
    }

    // GET: Projects/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0) return NotFound();

        var project = await context.Projects.FindAsync(id);
        
        if (project == null)
        {
            return NotFound();
        }
        
        return View(project);
    }

    // POST: Projects/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Project model)
    {
        if (id != model.Id) return NotFound();

        if (!ModelState.IsValid) return View(model);
        
        context.Update(model);
        await context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));
    }

    // GET: Projects/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return NotFound();

        var project = await context.Projects.FindAsync(id);
        
        if (project == null) return NotFound();
        
        return View(project);
    }

    // POST: Projects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var project = await context.Projects.FindAsync(id);
        
        if (project != null) context.Projects.Remove(project);
        await context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));
    }

    // GET: Projects/Details/5
    public async Task<IActionResult> Details(int id)
    {
        if (id == 0) return NotFound();

        var project = await context.Projects.FindAsync(id);
        
        if (project == null) return NotFound();

        return View(project);
    }
}