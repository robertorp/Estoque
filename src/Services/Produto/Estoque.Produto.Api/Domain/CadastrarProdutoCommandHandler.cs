using Estoque.Core.Messages;
using Estoque.Produto.Api.Data;
using MediatR;

namespace Estoque.Produto.Api.Domain;

public class CadastrarProdutoCommandHandler : CommandHandler, IRequestHandler<CadastrarProdutoCommand, GenericResponse>
{
    private readonly ProdutoContext _produtoContext;

    public CadastrarProdutoCommandHandler(ProdutoContext produtoContext)
    {
        _produtoContext = produtoContext;
    }

    public async Task<GenericResponse> Handle(CadastrarProdutoCommand request, CancellationToken cancellationToken)
    {
        if (request.IsInvalid())
            return Error(request.ValidationResult);

        var produto = new Produto(request.Nome, request.UnidadeMedida, request.PrecoVenda);

        await _produtoContext.Produtos.AddAsync(produto);

        var commitSucesso = await SaveEntitiyChanges(_produtoContext);

        return commitSucesso.IsValid ? Success(produto) : Error(commitSucesso);
    }
}