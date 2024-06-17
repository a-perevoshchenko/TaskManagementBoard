using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task.Management.Board.Models;

namespace Task.Management.Board.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Issue> Issues { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Project>()
            .HasMany(p => p.Members)
            .WithMany(u => u.Projects);

        builder.Entity<Issue>()
            .HasMany(i => i.Comments)
            .WithOne(c => c.Issue)
            .HasForeignKey(c => c.IssueId);

        builder.Entity<Issue>()
            .HasOne(i => i.AssignedToUser)
            .WithMany()
            .HasForeignKey(i => i.AssignedToUserId);

        builder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId);
    }
}