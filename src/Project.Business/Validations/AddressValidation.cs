using FluentValidation;
using Project.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("The {PropertyName} field must be filled.")
                .Length(2, 200).WithMessage("The {PropertyName} field, must be between {MinLength} e {MaxLength} characters");

            RuleFor(a => a.Number)
                .NotEmpty().WithMessage("The {PropertyName} field must be filled")
                .Length(1, 10).WithMessage("The {PropertyName} field, must be between {MinLength} and {MaxLength} characters.");

            RuleFor(a => a.Neighborhood)
                .NotEmpty().WithMessage("The {PropertyName} field must be filled")
                .Length(2, 150).WithMessage("The {PropertyName} field, must be between {MinLength} and {MaxLength} characters");

            RuleFor(a => a.Cep)
                .NotEmpty().WithMessage("The {PropertyName} field must be filled")
                .Length(8).WithMessage("The {PropertyName} field, must have {MaxLength} digits.");

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("The {PropertyName} field must be filled")
                .Length(2, 150).WithMessage("The {PropertyName} field, must be between {MinLength} and {MaxLength} characters");

            RuleFor(a => a.State)
                .NotEmpty().WithMessage("The {PropertyName} field must be filled")
                .Length(2, 150).WithMessage("The {PropertyName} field, must be between {MinLength} and {MaxLength} characters");

            RuleFor(a => a.Country)
                .NotEmpty().WithMessage("The {PropertyName} field must be filled")
                .Length(2, 150).WithMessage("The {PropertyName} field, must be between {MinLength} and {MaxLength} characters");
        }
    }
}
