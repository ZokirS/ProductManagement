using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.DAL.Repositories.Abstract;
using ProductManagement.DAL.Repositories;
using Microsoft.Extensions.Configuration;

namespace ProductManagement.DAL.Helpers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    opt => opt.MigrationsAssembly("ProductManagement.API")));
            services.EnsureMigrationOfContext<AppDbContext>();
            services.AddScoped<IProductAuditRepository, ProductAuditRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void EnsureMigrationOfContext<T>(this IServiceCollection services) where T : DbContext
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<T>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}