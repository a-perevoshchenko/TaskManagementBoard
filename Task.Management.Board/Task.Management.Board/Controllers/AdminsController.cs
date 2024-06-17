using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task.Management.Board.DTOs;
using Task.Management.Board.Models;

namespace Task.Management.Board.Controllers;


[Authorize(Roles = "Admin")]
public class AdminsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : Controller
{
    // GET: Admins/Index
    public async Task<IActionResult> Index()
    {
        var users = userManager.Users.ToList();
        var userRolesViewModel = new List<UserRolesViewModel>();

        foreach (var user in users)
        {
            var thisViewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Roles = await GetUserRoles(user)
            };
            userRolesViewModel.Add(thisViewModel);
        }

        return View(userRolesViewModel);
    }

    
    private async Task<List<string>> GetUserRoles(ApplicationUser user)
    {
        return [..await userManager.GetRolesAsync(user)];
    }
    
    // GET: Admins/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admins/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RegisterDto model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };
        var result = await userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "CommonUser");
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    // GET: Admins/Edit/5
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null) return NotFound();

        var user = await userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var model = new EditUserRolesViewModel
        {
            UserId = user.Id,
            Email = user.Email!,
            FullName = user.FullName,
            Roles = await GetUserRoles(user),
            AllRoles = roleManager.Roles.Select(r => r.Name).ToList()
        };

        return View(model);
    }

    // POST: Admins/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditUserRolesViewModel model)
    {
        var user = await userManager.FindByIdAsync(model.UserId);
        if (user == null) return NotFound();

        user.Email = model.Email;
        user.UserName = model.Email;
        user.FullName = model.FullName;

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        var currentRoles = await userManager.GetRolesAsync(user);
        var rolesToAdd = model.SelectedRoles.Except(currentRoles).ToList();
        var rolesToRemove = currentRoles.Except(model.SelectedRoles).ToList();

        await userManager.AddToRolesAsync(user, rolesToAdd);
        await userManager.RemoveFromRolesAsync(user, rolesToRemove);

        return RedirectToAction(nameof(Index));
    }

    // GET: Admins/Delete/5
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null) return NotFound();

        var user = await userManager.FindByIdAsync(id);

        if (user == null) return NotFound();

        return View(user);
    }

    // POST: Admins/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null) return NotFound();

        var result = await userManager.DeleteAsync(user);

        if (result.Succeeded) return RedirectToAction(nameof(Index));

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(user);
    }

    // GET: Admin/Details/5
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null) return NotFound();

        var user = await userManager.FindByIdAsync(id);

        if (user == null) return NotFound();

        return View(user);
    }
}