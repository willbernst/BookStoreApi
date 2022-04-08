using FluentValidation;
using Project.Business.Models;
using Project.Business.Validations.Documents;

namespace Project.Business.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("The {PropertyName} must be filled")
                .Length(2, 250).WithMessage("The {PropertyName} field, must be between {MinLength} and {MaxLength} characters");

            When(s => s.SupplierType == SupplierType.PhysicalPerson, () =>
            {
                RuleFor(s => s.Document.Length).Equal(CpfValidation.SizeCpf)
                    .WithMessage("The Document Field must be {ComparisonValue} characters and only {PropertyValue} was provided.");
                RuleFor(s => CpfValidation.Validate(s.Document)).Equal(true)
                    .WithMessage("The document provided is invalid");
            });


            When(s => s.SupplierType == SupplierType.LegalEntity, () =>
            {
                RuleFor(s => s.Document.Length).Equal(CnpjValidation.SizeCnpj)
                    .WithMessage("The Document Field must be {ComparisonValue} characters and only {PropertyValue} was provided.");
                RuleFor(s => CnpjValidation.Validate(s.Document)).Equal(true)
                    .WithMessage("The document provided is invalid");
            });
        }
    }
}


