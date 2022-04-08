using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.API.Dto
{
    public class SupplierDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [StringLength(14, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 11)]
        public string Document { get; set; }

        public int SupplierType { get; set; }

        public AddressDto Address { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<BookDto> Books { get; set; }
    }
}
