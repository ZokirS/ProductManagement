using ProductManagement.DAL.Models;

namespace ProductManagement.DAL.Repositories.Abstract
{
    public interface IProductAuditRepository
    {
        Task<List<ProductAudit>> GetAuditLogs(DateTime? startDate, DateTime? endDate);
    }
}