using Microsoft.EntityFrameworkCore;
using WorksAway.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Define DbSet properties for each model
    public DbSet<Admin> Admins { get; set; }
    public required DbSet<Employee> Employees { get; set; }
}