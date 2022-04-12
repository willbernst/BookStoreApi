using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.API.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project.API.Dto
{
    //uncomment this line if you use IFormFile in ImageUpload
    //[ModelBinder(BinderType = typeof(BookModelBinder))]
    public class BookDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string SupplierId { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(200, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(500, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 2)]
        public string Resume { get; set; }

        //Change to IFormFile if u want to use another upload type 
        public string ImageUpload { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 2)]
        public string Author { get; set; }

        public string Volume { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string Category { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Publication { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 2)]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string Price { get; set; }

        public bool InStock { get; set; }

        [ScaffoldColumn(false)]
        public string SupplierName { get; set; }
    }
}
