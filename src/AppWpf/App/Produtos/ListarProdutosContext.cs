using System.Collections.ObjectModel;
using Estoque.Produto.Api.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace AppWpf.Produtos;

public partial class ListarProdutosContext : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    public ListarProdutosContext(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ObservableCollection<ProdutoViewModel> Items { get; set; } = new ObservableCollection<ProdutoViewModel>();
    
    public async void Inicializando()
    {
        using var scoped = _serviceProvider.CreateScope();
        var produtoAppService = _serviceProvider.GetService<IProdutoAppService>();

        Items.Clear();

        var itens = await produtoAppService.ObterProdutos();

        foreach (var item in itens)
        {
            Items.Add(item);
        }
    }

    [ICommand]
    public void EditarProduto(ProdutoViewModel produtoViewModel)
    {
        var novoProdutoContent = _serviceProvider.GetService<NovoProdutoContent>();
        novoProdutoContent.AlterarProduto(produtoViewModel);

        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow.AbrirConteudo(novoProdutoContent);
    }
}