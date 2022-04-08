using Project.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Interfaces
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<Address> GetAddressBySupplier(Guid supplierId);
    }
}
