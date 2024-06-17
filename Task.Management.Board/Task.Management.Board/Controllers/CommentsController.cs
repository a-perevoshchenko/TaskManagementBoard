using Microsoft.AspNetCore.Mvc;
using Task.Management.Board.Data;
using Task.Management.Board.DTOs;
using Task.Management.Board.Models;

namespace Task.Management.Board.Controllers;

public class CommentsController(ApplicationDbContext context) : Controller
{
    // GET: Comments/Index
    public IActionResult Index()
    {
        var comments = context.Comments.ToList();
        return View(comments);
    }
    
    // GET: Comments/Create
    public IActionResult Create(int issueId)
    {
        var model = new CommentDto { IssueId = issueId };
        
        return View(model);
    }

    // POST: Comments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CommentDto model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var comment = new Comment
        {
            Content = model.Content,
            IssueId = model.IssueId,
            UserId = model.UserId
        };
            
        context.Comments.Add(comment);
        await context.SaveChangesAsync();
            
        return RedirectToAction("Details", "Issues", new { id = model.IssueId });
    }

    // GET: Comments/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0) return NotFound();

        var comment = await context.Comments.FindAsync(id);
        
        if (comment == null) return NotFound();

        var model = new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            IssueId = comment.IssueId,
            UserId = comment.UserId
        };
        
        return View(model);
    }

    // POST: Comments/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CommentDto model)
    {
        if (id != model.Id) return NotFound();

        if (!ModelState.IsValid) return View(model);
        
        var comment = await context.Comments.FindAsync(id);
        
        if (comment == null) return NotFound();
        
        comment.Content = model.Content;
        await context.SaveChangesAsync();
        
        return RedirectToAction("Details", "Issues", new { id = model.IssueId });
    }

    // GET: Comments/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return NotFound();

        var comment = await context.Comments.FindAsync(id);
        
        if (comment == null) return NotFound();

        return View(comment);
    }

    // POST: Comments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var comment = await context.Comments.FindAsync(id);
        
        if (comment == null) return View(comment);
        
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
        return RedirectToAction("Details", "Issues", new { id = comment.IssueId });

    }

    // GET: Comments/Details/5
    public async Task<IActionResult> Details(int id)
    {
        if (id == 0) return NotFound();

        var comment = await context.Comments.FindAsync(id);
        
        if (comment == null) return NotFound();

        return View(comment);
    }
}