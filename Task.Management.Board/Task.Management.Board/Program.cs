using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task.Management.Board.Data;
using Task.Management.Board.Helpers;
using Task.Management.Board.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllersWithViews().AddNewtonsoftJson();

builder.Services.AddControllersWithViews();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "account",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "dashboard",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
await RoleInitializer.CreateRoles(roleManager, userManager);

app.Run();