using System.Reflection.Metadata.Ecma335;
using Estoque.Core.Messages;
using Estoque.Produto.Api.Data;
using Estoque.Produto.Api.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Produto.Api.Application;

public class ProdutoAppService : IProdutoAppService
{
    private readonly ProdutoContext _produtoContext;
    private readonly IMediator _mediator;

    public ProdutoAppService(ProdutoContext produtoContext, IMediator mediator)
    {
        _produtoContext = produtoContext;
        _mediator = mediator;
    }

    public async Task<IEnumerable<ProdutoViewModel>> ObterProdutos()
    {
        return await _produtoContext.Produtos.Select(p => new ProdutoViewModel(p)).ToListAsync();
    }

    public async Task<GenericResponse> AdicionarProduto(ProdutoViewModel produtoViewModel)
    {
        return await _mediator.Send(new CadastrarProdutoCommand
        {
            UnidadeMedida = produtoViewModel.UnidadeMedida,
            PrecoVenda = produtoViewModel.PrecoVenda,
            Nome = produtoViewModel.Nome
        }, CancellationToken.None);
    }

    public async Task<GenericResponse> AtualizarProduto(ProdutoViewModel produtoViewModel)
    {
        return await _mediator.Send(new AlterarProdutoCommand
        {
            Id = produtoViewModel.Id,
            UnidadeMedida = produtoViewModel.UnidadeMedida,
            PrecoVenda = produtoViewModel.PrecoVenda,
            Nome = produtoViewModel.Nome
        }, CancellationToken.None);
    }
}