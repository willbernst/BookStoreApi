using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project.API.Dto;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.API.Extensions
{
    //Custom binder for sending IFormFile and ViewModel(DTO) inside a FormData
    //public class BookModelBinder : IModelBinder
    //{
    //    public Task BindModelAsync(ModelBindingContext bindingContext)
    //    {
    //        if(bindingContext == null)
    //        {
    //            throw new ArgumentNullException(nameof(bindingContext));
    //        }

    //        var serializeOptions = new JsonSerializerOptions
    //        {
    //            WriteIndented = true,
    //            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    //            PropertyNameCaseInsensitive = true,
    //        };
    //
    //        var bookDto = JsonSerializer.Deserialize<BookDto>(bindingContext.ValueProvider.GetValue("book").FirstOrDefault(), serializeOptions);
    //        bookDto.ImageUpload = bindingContext.ActionContext.HttpContext.Request.Form.Files.FirstOrDefault();
    //
    //        bindingContext.Result = ModelBindingResult.Success(bookDto);
    //        return Task.CompletedTask;
    //    }
    //}
}
