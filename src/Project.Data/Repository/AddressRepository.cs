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
    public class AddressRepository : Repository<Address>, IAddressRepository
    {

        public AddressRepository(MyDbContext context) : base(context) { }

        public async Task<Address> GetAddressBySupplier(Guid supplierId)
        {
            return await _dbContext.Addresses.AsNoTracking().FirstOrDefaultAsync(f => f.SupplierId == supplierId);
        }
    }
}
