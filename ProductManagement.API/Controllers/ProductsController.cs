using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.FilterAttributes;
using ProductManagement.BL.Models.Product;
using ProductManagement.BL.Services.Abstract;
using ProductManagement.DAL.Helpers;
using ProductManagement.DAL.Models;

namespace ProductManagement.API.Controllers
{
    public class ProductsController(IProductService productService): ControllerBase
    {
        [HttpGet("[controller]/{id}")]
        [AuthorizeRole(UserRole.Admin, UserRole.User)]
        public async Task<ActionResult<ProductVAT>> GetProductById([FromQuery] long id)
        {
            var product = await productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("[controller]")]
        [AuthorizeRole(UserRole.Admin, UserRole.User)]
        public async Task<ActionResult<List<ProductVAT>>> GetProducts()
        {
            var products = await productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpPost("[controller]")]
        [AuthorizeRole(UserRole.Admin)]
        public async Task<ActionResult<Product>> AddProduct([FromBody] ProductAddRequest request)
        {
            if (HttpContext.Items["User"] is not User user) return Unauthorized();

            var product = await productService.AddProductAsync(user.Id, request);
            return Ok(product);
        }

        [HttpPut("[controller]/{id}")]
        [AuthorizeRole(UserRole.Admin)]
        public async Task<ActionResult<Product>> UpdateProduct([FromQuery] long id, [FromBody] ProductAddRequest request)
        {
            if (HttpContext.Items["User"] is not User user) return Unauthorized();

            ProductUpdateRequest updRequest = (ProductUpdateRequest)request;
            updRequest.Id = id;

            var product = await productService.UpdateProductAsync(user.Id, updRequest);
            return Ok(product);
        }

        [HttpDelete("[controller]/{id}")]
        [AuthorizeRole(UserRole.Admin)]
        public async Task<ActionResult<Product>> DeleteProduct([FromQuery] long id)
        {
            if (HttpContext.Items["User"] is not User user) return Unauthorized();

            var product = await productService.DeleteProductAsync(user.Id, id);
            return Ok(product);
        }
    }
}
