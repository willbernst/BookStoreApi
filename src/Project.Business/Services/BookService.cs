using Project.Business.Interfaces;
using Project.Business.Models;
using Project.Business.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUser _user;

        public BookService(IBookRepository bookRepository, ICommunicator communicator, IUser user) :  base(communicator)
        {
            _bookRepository = bookRepository;
            _user = user;
        }

        public async Task Add(Book book)
        {
            if (!ExecuteValidations(new BookValidation(), book)) return;
            await _bookRepository.Add(book);
        }

        public async Task Update(Book book)
        {
            if(!ExecuteValidations(new BookValidation(), book)) return;
            await _bookRepository.Update(book);
        }

        public async Task Remove(Guid bookId)
        {
            await _bookRepository.Remove(bookId);
        }

        public void Dispose()
        {
            _bookRepository?.Dispose();
        }
    }
}
