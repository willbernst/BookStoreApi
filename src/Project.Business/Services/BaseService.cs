using Project.Business.Interfaces;
using FluentValidation.Results;
using Project.Business.Notifications;
using FluentValidation;
using Project.Business.Models;

namespace Project.Business.Services
{
    public class BaseService
    {
        private readonly ICommunicator _communicator;

        protected BaseService(ICommunicator communicator)
        {
            _communicator = communicator;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach(var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {
            _communicator.Handle(new Notification(message));
        }

        protected bool ExecuteValidations<Tvalidation, TEntity>(Tvalidation validation, TEntity entity) where Tvalidation : AbstractValidator<TEntity> where TEntity : Entity
        {
            var validator = validation.Validate(entity);
            if (validator.IsValid) return true;
            
            Notify(validator);

            return false;
        }
    }
}
