using FuckWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuckWeb.Data;

public class FDbContext : DbContext
{
    public FDbContext(DbContextOptions options): base(options)
    {
            
    }
    public DbSet<User> Users { get; set; }
    public DbSet<FingerCheck> FingerChecks { get; set; }
}