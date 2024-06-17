using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task.Management.Board.DTOs;
using Task.Management.Board.Models;

namespace Task.Management.Board.Controllers;

public class AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager): Controller
{
    // GET: /Accounts/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Accounts/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await userManager.CreateAsync(user, model.Password);
        
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "CommonUser");
            await signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(model);
    }

    // GET: /Accounts/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Accounts/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
        
        if (result.Succeeded) return RedirectToAction("Index", "Home");
        
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        
        return View(model);
    }

    // GET: /Account/Logout
    public IActionResult Logout()
    {
        return View();
    }

    // POST: /Account/Logout
    [HttpPost, ActionName("Logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogoutConfirmed()
    {
        await signInManager.SignOutAsync();
        
        return RedirectToAction("Index", "Home");
    }
}