using Estoque.Core.DomainObjects;

namespace Estoque.Produto.Api.Domain;

public class Produto : Entity, IAggregateRoot
{
    public Produto(string nome, string unidadeMedida, decimal precoVenda)
    {
        Nome = nome;
        UnidadeMedida = unidadeMedida;
        PrecoVenda = precoVenda;
    }

    public string Nome { get; private set; }

    public string UnidadeMedida { get; private set; }

    public decimal PrecoVenda { get; private set; }

    public void Alterar(string nome, string unidadeMedida, decimal precoVenda)
    {
        Nome = nome;
        UnidadeMedida = unidadeMedida;
        PrecoVenda = precoVenda;
    }
}