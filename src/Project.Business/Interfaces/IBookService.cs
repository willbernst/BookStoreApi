using Project.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Interfaces
{
    public interface IBookService : IDisposable
    {
        Task Add(Book book);
        Task Update(Book book);
        Task Remove(Guid bookId);
    }
}
