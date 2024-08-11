using ProductManagement.BL.Models.Product;
using ProductManagement.DAL.Models;

namespace ProductManagement.BL.Services.Abstract;

public interface IProductService
{
    Task<ProductVAT> GetProductByIdAsync(long id);
     Task<Product> AddProductAsync(long userId, ProductAddRequest request);
    Task<Product> UpdateProductAsync(long userId, ProductUpdateRequest request);
    Task<Product> DeleteProductAsync(long userId, long id);
    Task<List<ProductVAT>> GetProductsAsync();
}

