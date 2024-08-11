using ProductManagement.BL.Models.ProductAudit;
using ProductManagement.DAL.Models;

namespace ProductManagement.BL.Services.Abstract
{
    public interface IProductAuditService
    {
        Task<List<ProductAudit>> GetProductAudits(DateTime? startDate, DateTime? endDate);
    }
}