using System.Reflection.Metadata.Ecma335;
using Estoque.Core.Communication.Mediator;
using Estoque.Core.Messages;
using Estoque.Produto.Api.Data;
using Estoque.Produto.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Produto.Api.Application;

public class ProdutoAppService : IProdutoAppService
{
    private readonly ProdutoContext _produtoContext;
    private readonly IMediatorHandler _mediatorHandler;

    public ProdutoAppService(ProdutoContext produtoContext, IMediatorHandler mediatorHandler)
    {
        _produtoContext = produtoContext;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<IEnumerable<ProdutoViewModel>> ObterProdutos()
    {
        return await _produtoContext.Produtos.Select(p => new ProdutoViewModel(p)).ToListAsync();
    }

    public async Task<GenericResponse> AdicionarProduto(ProdutoViewModel produtoViewModel)
    {
        return await _mediatorHandler.EnviarComando(new CadastrarProdutoCommand
        {
            UnidadeMedida = produtoViewModel.UnidadeMedida,
            PrecoVenda = produtoViewModel.PrecoVenda,
            Nome = produtoViewModel.Nome
        });
    }

    public async Task<GenericResponse> AtualizarProduto(ProdutoViewModel produtoViewModel)
    {
        return await _mediatorHandler.EnviarComando(new AlterarProdutoCommand
        {
            Id = produtoViewModel.Id,
            UnidadeMedida = produtoViewModel.UnidadeMedida,
            PrecoVenda = produtoViewModel.PrecoVenda,
            Nome = produtoViewModel.Nome
        });
    }
}