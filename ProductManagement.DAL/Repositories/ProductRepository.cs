using Microsoft.EntityFrameworkCore;
using ProductManagement.DAL.Helpers.Extensions;
using ProductManagement.DAL.Models;
using ProductManagement.DAL.Repositories.Abstract;
using static ProductManagement.Common.Helpers.StringConstants;

namespace ProductManagement.DAL.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<Product> AddProductAsync(long userId, Product product)
    {
        product.CreatedAt = DateTime.UtcNow;
        await context.Products.AddAsync(product);
        var productAudit = new ProductAudit
        {
            UserId = userId,
            LogMessage = string.Format(ProductAuditAction.Create, product.Id),
            CreatedAt = DateTime.UtcNow
        };
        await context.ProductAudits.AddAsync(productAudit);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> DeleteProductAsync(long userId, long id)
    {
        var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (existingProduct is null) return null;

        var productAudit = new ProductAudit
        {
            UserId = userId,
            LogMessage = string.Format(ProductAuditAction.Delete, id),
            CreatedAt = DateTime.UtcNow
        };
        context.ProductAudits.Add(productAudit);
        context.Products.Remove(existingProduct);
        await context.SaveChangesAsync();
        return existingProduct;
    }

    public  Task<Product?> GetProductByIdAsync(long id) =>
        context.Products.FirstOrDefaultAsync(p => p.Id == id);

    public Task<List<Product>> GetProductsAsync() =>
        context.Products.ToListAsync();

    public Task<Dictionary<long, Product>> GetProductsByIdsAsync(List<long> ids) => 
        context.Products.Where(p => ids.Contains(p.Id)).ToDictionaryAsync(p => p.Id);

    public async Task<List<Product>> GetProductsByTitleAsync(string name)
    {
        var products = await context.Products.ToListAsync();
        return products.Where(p => p.Title.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public async Task<Product?> UpdateProductAsync(long userId,Product product)
    {
        var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        if (existingProduct is null) return null;

        var changedProps = existingProduct.CopyProperties(product);
        foreach (var property in changedProps)
        {
            var productAudit = new ProductAudit
            {
                UserId = userId,
                LogMessage = string.Format(ProductAuditAction.Update, existingProduct.Id, property),
                CreatedAt = DateTime.UtcNow,
            };
            context.ProductAudits.Add(productAudit);
        }
        existingProduct.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return existingProduct;
    }
}

