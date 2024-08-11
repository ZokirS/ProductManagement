using ProductManagement.BL.Models.ProductAudit;
using ProductManagement.BL.Services.Abstract;
using ProductManagement.DAL.Models;
using ProductManagement.DAL.Repositories.Abstract;

namespace ProductManagement.BL.Services
{
    public class ProductAuditService(IProductAuditRepository repository) : IProductAuditService
    {

        public Task<List<ProductAudit>> GetProductAudits(DateTime? startDate, DateTime? endDate)=>
            repository.GetAuditLogs(startDate, endDate);
    }
}