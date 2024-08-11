using Microsoft.Extensions.Configuration;
using ProductManagement.BL.Models.Product;
using ProductManagement.BL.Services.Abstract;
using ProductManagement.Common.Exceptions.CustomExceptions;
using ProductManagement.DAL.Helpers.Extensions;
using ProductManagement.DAL.Models;
using ProductManagement.DAL.Repositories.Abstract;

namespace ProductManagement.BL.Services;

public class ProductService(IConfiguration configuration, IProductRepository productRepository, IProductAuditRepository productAuditRepository) : IProductService
{
    private readonly decimal _vat = Convert.ToDecimal(configuration["ProductSetting:VAT"]);

    public async Task<Product> AddProductAsync(long userId, ProductAddRequest request)
    {
        var product = new Product();
        product.CopyPropertiesFrom(request);
        
        var result = await productRepository.AddProductAsync(userId,product);
        
        return result;
    }

    public async Task<Product> DeleteProductAsync(long userId, long id)
    {
        var product = await productRepository.DeleteProductAsync(userId,id);
        if (product is null)
            throw new ProductNotFoundException(id);

        return product;
    }

    public async Task<ProductVAT> GetProductByIdAsync(long id)
    {
        var product = await productRepository.GetProductByIdAsync(id);
        if (product is null)
            throw new ProductNotFoundException(id);

        var productWithVAT = new ProductVAT
        {
            Title = product.Title,
            Quantity = product.Quantity,
            Price = product.Price,
            TotalPriceWithVAT = product.Quantity * product.Price * (1 + _vat),
        };
        return productWithVAT;
    }

    public async Task<List<ProductVAT>> GetProductsAsync()
    {
        var products = await productRepository.GetProductsAsync();
        return products.Select(p => new ProductVAT
        {
            Title = p.Title,
            Quantity = p.Quantity,
            Price = p.Price,
            TotalPriceWithVAT = p.Quantity * p.Price * (1 + _vat),
        }).ToList();
    }

    public Task<Dictionary<long, Product>> GetProductsByIdsAsync(List<long> ids) =>
        productRepository.GetProductsByIdsAsync(ids);

    public Task<List<Product>> GetProductsByTitleAsync(ProductTitleRequest request) =>
        productRepository.GetProductsByTitleAsync(request.Title);

    public async Task<Product> UpdateProductAsync(long userId, ProductUpdateRequest request)
    {
        var product = new Product();
        product.CopyPropertiesFrom(request);
        var exProduct = await GetProductByIdAsync(request.Id);

        var updatedProduct = await productRepository.UpdateProductAsync(userId,product);
        if (updatedProduct is null)
            throw new ProductNotFoundException(product.Id);

        return updatedProduct;
    }
}
