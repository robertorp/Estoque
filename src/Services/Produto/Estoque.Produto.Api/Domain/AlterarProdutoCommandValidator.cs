using FluentValidation;

namespace Estoque.Produto.Api.Domain;

public class AlterarProdutoCommandValidator : AbstractValidator<AlterarProdutoCommand>
{
    public AlterarProdutoCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();

        RuleFor(p => p.Nome)
            .MinimumLength(2)
            .MaximumLength(250)
            .NotEmpty();

        RuleFor(p => p.UnidadeMedida)
            .MinimumLength(2)
            .MaximumLength(10)
            .NotEmpty();

        RuleFor(p => p.PrecoVenda)
            .GreaterThan(0)
            .NotEmpty();
    }
}