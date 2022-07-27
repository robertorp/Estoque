using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Estoque.Core.Messages;

public abstract class Command : Message, IRequest<GenericResponse>
{
    public DateTime Timestamp { get; private set; }
    [JsonIgnore] public ValidationResult ValidationResult { get; private set; }

    protected Command()
    {
        Timestamp = DateTime.UtcNow;
        ValidationResult = new ValidationResult();
    }

    protected bool Validate<TValidator>(AbstractValidator<TValidator> validator)
        where TValidator : class
    {
        ValidationResult = validator.Validate(this as TValidator);
        return ValidationResult.IsValid;
    }

    public virtual bool IsValid()
    {
        return ValidationResult.IsValid;
    }

    public bool IsInvalid()
    {
        return !IsValid();
    }
}