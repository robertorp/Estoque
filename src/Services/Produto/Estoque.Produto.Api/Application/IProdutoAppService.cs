using Estoque.Core.Messages;

namespace Estoque.Produto.Api.Application;

public interface IProdutoAppService
{
    Task<IEnumerable<ProdutoViewModel>> ObterProdutos();
    Task<GenericResponse> AdicionarProduto(ProdutoViewModel produtoViewModel);
    Task<GenericResponse> AtualizarProduto(ProdutoViewModel produtoViewModel);
}