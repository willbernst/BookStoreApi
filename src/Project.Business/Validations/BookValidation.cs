using FluentValidation;
using Project.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Validations
{
    public class BookValidation : AbstractValidator<Book>
    {
        public BookValidation()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("The {PropertyName} field must be filled.")
                .Length(2, 250).WithMessage("The {PropertyName} field, must be between {MinLength} and {MaxLength} characters");

            RuleFor(b => b.Resume)
               .NotEmpty().WithMessage("The {PropertyName} field must be filled.")
               .Length(50, 1000).WithMessage("The {PropertyName} field, must be between {MinLength} and {MaxLength} characters");

            RuleFor(b => b.Author)
                .NotEmpty().WithMessage("The {PropertyName} field must be filled.")
                .Length(2, 150).WithMessage("The {PropertyName} field, must be between {MinLength} and {MaxLength} characters");

            RuleFor(b => b.Category)
                .NotEmpty().WithMessage("The {PropertyName} field must be filled");

            RuleFor(b => b.Publication)
                .NotEmpty().WithMessage("The {PropertyName} field must be filled");

            RuleFor(b => b.Publisher)
                .NotEmpty().WithMessage("The {PropetyName} field, must be filled")
                .Length(5, 150).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters");

            RuleFor(b => b.Price)
                .GreaterThan(0).WithMessage("The {PropertyName} field must be greater than {ComparisonValue}");

            RuleFor(b => b.InStock)
                .NotEmpty().WithMessage("The {PropertyName} field, must be filled");
        }
    }
}
