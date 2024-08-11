using ProductManagement.DAL.Models;

namespace ProductManagement.DAL.Repositories.Abstract;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(long id);
    Task<Product> AddProductAsync(long userId, Product product);
    Task<Product?> UpdateProductAsync(long userId, Product product);
    Task<Product?> DeleteProductAsync(long userId, long id);
    Task<List<Product>> GetProductsAsync();
    Task<List<Product>> GetProductsByTitleAsync(string name);
    Task<Dictionary<long, Product>> GetProductsByIdsAsync(List<long> ids);
}

