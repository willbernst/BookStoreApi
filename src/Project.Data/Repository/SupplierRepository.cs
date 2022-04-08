using Microsoft.EntityFrameworkCore;
using Project.Business.Interfaces;
using Project.Business.Models;
using Project.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(MyDbContext context) : base(context) { }

        public async Task<Supplier> GetSupplierAddress(Guid supplierId)
        {
            return await _dbContext.Suppliers.AsNoTracking().Include(s => s.Address).FirstOrDefaultAsync(s => s.Id == supplierId);
        }

        public async Task<Supplier> GetSupplierProductsAddress(Guid supplierId)
        {
            return await _dbContext.Suppliers.AsNoTracking().Include(s => s.Books).Include(s => s.Address).FirstOrDefaultAsync(s => s.Id == supplierId);
        }
    }
}
