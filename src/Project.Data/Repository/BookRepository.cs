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
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(MyDbContext context) : base(context) { }


        public async Task<Book> GetBooksSupplier(Guid supplierId)
        {
            return await _dbContext.Books.AsNoTracking().Include(b => b.Supplier).FirstOrDefaultAsync(b => b.Id == supplierId);
        }

        public async Task<IEnumerable<Book>> GetBooksSuppliers()
        {
            return await _dbContext.Books.AsNoTracking().Include(b => b.Supplier).OrderBy(b => b.Title).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksBySuppliers(Guid supplierId)
        {
            return await Search(s => s.SupplierId == supplierId);
        }
    }
}
