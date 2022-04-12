using Project.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksBySuppliers(Guid supplierId);
        Task<IEnumerable<Book>> GetBooksSuppliers();
        Task<Book> GetBooksSupplier(Guid supplierId);
    }
}
