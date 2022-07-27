using Estoque.Core.Messages;
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

public class AlterarProdutoCommand : Command
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public string UnidadeMedida { get; set; }

    public decimal PrecoVenda { get; set; }

    public override bool IsValid()
    {
        return Validate(new AlterarProdutoCommandValidator());
    }
}