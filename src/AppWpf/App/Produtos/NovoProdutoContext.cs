using Estoque.Produto.Api.Application;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace AppWpf.Produtos;

public partial class NovoProdutoContext : ObservableRecipient
{
    private readonly IServiceProvider _serviceProvider;

    public NovoProdutoContext(
        IServiceProvider serviceProvider
    )
    {
        _serviceProvider = serviceProvider;
    }

    [ObservableProperty]
    private string _nome;

    [ObservableProperty]
    private string _unidadeMedida;

    [ObservableProperty]
    private decimal _precoVenda;

    [ObservableProperty]
    private bool _estaAdicionarProduto = true;

    [ObservableProperty]
    private bool _estaAlterarProduto = false;


    private ProdutoViewModel _produtoViewModel;


    [ICommand]
    private async void AdicionarProduto()
    {
        using var scope = _serviceProvider.CreateScope();
        var produtoAppService = scope.ServiceProvider.GetRequiredService<IProdutoAppService>();

        var response = await produtoAppService.AdicionarProduto(new ProdutoViewModel
        {
            Nome = _nome,
            PrecoVenda = _precoVenda,
            UnidadeMedida = _unidadeMedida
        });

        if (response.Success == false)
        {
            Dialog.Show(new ValidationDialog(response));
            return;
        }


        Nome = string.Empty;
        PrecoVenda = 0.0m;
        UnidadeMedida = string.Empty;
        Dialog.Show(new TextDialog("Produto adicionado com sucesso"));
    }

    [ICommand]
    private async void AlterarProduto()
    {
        _produtoViewModel.PrecoVenda = PrecoVenda;
        _produtoViewModel.UnidadeMedida = UnidadeMedida;
        _produtoViewModel.Nome = Nome;

        using var scope = _serviceProvider.CreateScope();
        var produtoAppService = scope.ServiceProvider.GetRequiredService<IProdutoAppService>();

        var response = await produtoAppService.AtualizarProduto(_produtoViewModel);

        if (response.Success == false)
        {
            Dialog.Show(new ValidationDialog(response));
            return;
        }

        Dialog.Show(new TextDialog("Produto alterado com sucesso"));
    }

    public void AlterarProduto(ProdutoViewModel produtoViewModel)
    {
        Nome = produtoViewModel.Nome;
        PrecoVenda = produtoViewModel.PrecoVenda;
        UnidadeMedida = produtoViewModel.UnidadeMedida;

        _produtoViewModel = produtoViewModel;

        EstaAdicionarProduto = false;
        EstaAlterarProduto = true;
    }
}