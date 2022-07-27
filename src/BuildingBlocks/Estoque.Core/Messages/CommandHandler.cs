using Estoque.Core.Data;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Estoque.Core.Messages;

public class CommandHandler
{
    private readonly ValidationResult _validationResult = new();

    protected void AddError(string mensagem)
    {
        _validationResult.Errors.Add(new(string.Empty, mensagem));
    }

    protected void AddErrors(ValidationResult validationResult)
    {
        validationResult.Errors.ForEach(e => AddError(e.ErrorMessage));
    }

    protected static GenericResponse Success(object payload = null)
    {
        return GenericResponse.CreateSuccess(payload ?? Unit.Value);
    }

    protected GenericResponse Error(string message = null)
    {
        if (message != null)
        {
            AddError(message);
        }

        return GenericResponse.CreateError(_validationResult);
    }

    protected static GenericResponse Error(ValidationResult result)
    {
        return GenericResponse.CreateError(result);
    }

    protected async Task<ValidationResult> SaveEntitiyChanges(IUnitOfWork uow, string message)
    {
        if (_validationResult.Errors.Any()) return _validationResult;
        if (!await uow.SaveEntitiesAsync()) AddError(message);

        return _validationResult;
    }

    protected async Task<ValidationResult> SaveEntitiyChanges(IUnitOfWork uow)
    {
        return await SaveEntitiyChanges(uow, "Houve um problema ao salvar os dados!").ConfigureAwait(false);
    }

    protected async Task<GenericResponse> SaveEntitiyChanges(IUnitOfWork uow, object sucesso)
    {
        var result = await SaveEntitiyChanges(uow);
        if (!result.IsValid) return Error(result);

        return Success(sucesso);
    }
}