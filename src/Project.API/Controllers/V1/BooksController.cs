using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.API.Dto;
using Project.API.Extensions;
using Project.Business.Interfaces;
using Project.Business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Project.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/books")]
    public class BooksController : MainController
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BooksController(ICommunicator communicator, IBookRepository bookRepository, IBookService bookService,
                               IMapper mapper, IUser user, IHttpContextAccessor httpContextAccessor) : base(communicator, user)
        {
            _bookRepository = bookRepository;
            _bookService = bookService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<BookDto>>(await _bookRepository.GetBooksSuppliers());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookDto>> GetById(Guid id)
        {
            var bookDto = await GetBook(id);

            if (bookDto == null) return NotFound();

            return bookDto;
        }

        [ClaimAuthorize("Book", "Add")]
        [HttpPost]
        public async Task<ActionResult<BookDto>> Add(BookDto bookDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imageName = Guid.NewGuid() + "_" + bookDto.Image;
            if(!FileUpload(bookDto.ImageUpload, imageName))
            {
                return CustomResponse(bookDto);
            }

            bookDto.Image = imageName;
            await _bookService.Add(_mapper.Map<Book>(bookDto));

            return CustomResponse(bookDto);
        }

        [ClaimAuthorize("Book", "Update")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, BookDto bookDto)
        {
            if (id != bookDto.Id)
            {
                NotifyError("The informed id's are not the same");
                return CustomResponse();
            }

            var updateBook = await GetBook(id);

            if (string.IsNullOrEmpty(bookDto.Image))
                bookDto.Image = updateBook.Image;

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (bookDto.ImageUpload != null)
            {
                var nameImage = Guid.NewGuid() + "_" + bookDto.Image;
                if (!FileUpload(bookDto.ImageUpload, nameImage))
                {
                    return CustomResponse(ModelState);
                }

                updateBook.Image = nameImage;
            }

            updateBook.SupplierId = updateBook.SupplierId;
            updateBook.Title = bookDto.Title;
            updateBook.Resume = bookDto.Resume;
            updateBook.Author = bookDto.Author;
            updateBook.Volume = bookDto.Volume;
            updateBook.Category = bookDto.Category;
            updateBook.Publication = bookDto.Publication;
            updateBook.Publisher = bookDto.Publisher;
            updateBook.Price = bookDto.Price;
            updateBook.InStock = bookDto.InStock;

            await _bookService.Update(_mapper.Map<Book>(updateBook));

            return CustomResponse(bookDto);
        }

        [ClaimAuthorize("Book", "Remove")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<BookDto>> Remove(Guid id)
        {
            var book = await GetBook(id);

            if (book == null) return NotFound();

            await _bookService.Remove(id);

            return CustomResponse(book);
        }

        private async Task<BookDto> GetBook(Guid id)
        {
            return _mapper.Map<BookDto>(await _bookRepository.GetBooksSupplier(id));
        }

        private bool FileUpload(string file, string imgName)
        {
            if (string.IsNullOrEmpty(file))
            {
                NotifyError("Please, provide an image for this product");
                return false;
            }
            
            var imageDataByteArray = Convert.FromBase64String(imgName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imgName);

            if (System.IO.File.Exists(filePath))
            {
                NotifyError("A file with this name already exists");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }

        #region

        //[ClaimAuthorize("Book", "Add")]
        //[HttpPost]
        //public async Task<ActionResult<BookDto>> Add2(BookDto bookDto, [ModelBinder(BinderType = typeof(BookModelBinder))] BookModelBinder bookModelBinder)
        //{
        //   if (!ModelState.IsValid) return CustomResponse(ModelState);
        //
        //    var prefixImage = Guid.NewGuid() + "_";
        //    if (!FileUpload(bookDto.ImageUpload, prefixImage))
        //    {
        //       return CustomResponse(ModelState);
        //    }
        //
        //    bookDto.Image = prefixImage + bookDto.ImageUpload.FileName;
        //    await _bookService.Add(_mapper.Map<Book>(bookDto));

        //   return CustomResponse(bookDto);
        //}

        //[RequestSizeLimit(40000000)]
        //[HttpPost("image")]
        //public ActionResult AddImage(IFormFile file)
        //{
        //    return Ok(file);
        //}

        //private async Task<bool> FileUpload2(IFormFile file, string prefixImage)
        //{
        //   if(file == null || file.Length == 0)
        //    {
        //        NotifyError("Please, provide an image for this product");
        //        return false;
        //    }

        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", prefixImage, file.FileName);

        //    if (System.IO.File.Exists(path))
        //    {
        //        NotifyError("A file with this name already exists");
        //        return false;
        //    }

        //    using(var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    return true;
        //}

        #endregion OtherTypeOfUpdate
    }
}
