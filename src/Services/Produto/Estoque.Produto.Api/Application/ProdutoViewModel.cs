namespace Estoque.Produto.Api.Application;

public class ProdutoViewModel
{
    public ProdutoViewModel() {}
    public ProdutoViewModel(Domain.Produto produto)
    {
        Id = produto.Id;
        Nome = produto.Nome;
        UnidadeMedida = produto.UnidadeMedida;
        PrecoVenda = produto.PrecoVenda;
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string UnidadeMedida { get; set; }
    public decimal PrecoVenda { get; set; }
}