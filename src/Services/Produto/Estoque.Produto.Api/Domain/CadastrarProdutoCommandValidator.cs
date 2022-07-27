using FluentValidation;

namespace Estoque.Produto.Api.Domain;

public class CadastrarProdutoCommandValidator : AbstractValidator<CadastrarProdutoCommand>
{
    public CadastrarProdutoCommandValidator()
    {
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