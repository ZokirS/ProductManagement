using Microsoft.EntityFrameworkCore;
using ProductManagement.DAL.Models;
using ProductManagement.DAL.Repositories.Abstract;

namespace ProductManagement.DAL.Repositories
{
    public class ProductAuditRepository(AppDbContext context) : IProductAuditRepository
    {
        public Task<List<ProductAudit>> GetAuditLogs(DateTime? startDate, DateTime? endDate)
        {
            var query = context.ProductAudits.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(e => e.CreatedAt >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(e => e.CreatedAt <= endDate.Value);
            }

            return query.ToListAsync();
        }
    }
}