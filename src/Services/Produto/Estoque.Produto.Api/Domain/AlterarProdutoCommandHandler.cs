using Estoque.Core.Messages;
using Estoque.Produto.Api.Data;
using MediatR;

namespace Estoque.Produto.Api.Domain;

public class AlterarProdutoCommandHandler : CommandHandler, IRequestHandler<AlterarProdutoCommand, GenericResponse>
{
    private readonly ProdutoContext _produtoContext;

    public AlterarProdutoCommandHandler(ProdutoContext produtoContext)
    {
        _produtoContext = produtoContext;
    }

    public async Task<GenericResponse> Handle(AlterarProdutoCommand request, CancellationToken cancellationToken)
    {
        if (request.IsInvalid())
            return Error(request.ValidationResult);

        var produto = _produtoContext.Produtos.FirstOrDefault(p => p.Id == request.Id);

        if (produto == null) return Error("Produto não encontrado");

        produto.Alterar(request.Nome, request.UnidadeMedida, request.PrecoVenda);

        _produtoContext.Produtos.Update(produto);

        var commitSucesso = await SaveEntitiyChanges(_produtoContext);

        return commitSucesso.IsValid ? Success(produto) : Error(commitSucesso);
    }
}