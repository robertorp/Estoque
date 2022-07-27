using Estoque.Core.Messages;

namespace Estoque.Produto.Api.Domain;

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