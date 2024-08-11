
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.FilterAttributes;
using ProductManagement.BL.Services.Abstract;
using ProductManagement.DAL.Models;

namespace ProductManagement.API.Controllers
{
    public class ProductAuditController(IProductAuditService productAuditService): ControllerBase
    {
        [HttpGet("audit/{from:datetime?}/{to:datetime?}")]
        [AuthorizeRole(DAL.Helpers.UserRole.Admin)]
        public async Task<ActionResult<List<ProductAudit>>> ProductAudits([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var result = await productAuditService.GetProductAudits(from, to);
            return Ok(result);
        }
    }
}
