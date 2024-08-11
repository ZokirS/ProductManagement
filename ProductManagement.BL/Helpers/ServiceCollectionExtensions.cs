using Microsoft.Extensions.DependencyInjection;
using ProductManagement.BL.Services.Abstract;
using ProductManagement.BL.Services;

namespace ProductManagement.BL.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductAuditService, ProductAuditService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}