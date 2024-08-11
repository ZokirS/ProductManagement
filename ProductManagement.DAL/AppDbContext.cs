using Microsoft.EntityFrameworkCore;
using ProductManagement.DAL.Models;

namespace ProductManagement.DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ProductAudit> ProductAudits{ get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Enum>().HaveConversion<string>();
    }
}

