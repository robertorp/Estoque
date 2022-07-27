using FluentValidation.Results;

namespace Estoque.Core.Messages;

public class GenericResponse
{
    private GenericResponse(bool success, object payload = default, ValidationResult validationResult = null)
    {
        ValidationResult = validationResult;
        Success = success;
        Payload = payload;
    }

    public bool Success { get; }
    public object Payload { get; }
    public ValidationResult ValidationResult { get; }

    public T PayloadAs<T>()
    {
        if (Payload is not T payload) throw new InvalidCastException($"Não foi possível converter a carga útil para este tipo {typeof(T)}");

        return payload;
    }

    public static GenericResponse CreateSuccess(object payload)
    {
        return new(true, payload);
    }

    public static GenericResponse CreateError(ValidationResult result)
    {
        return new(false, default, result);
    }
}