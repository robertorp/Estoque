using System.Windows;
using System.Windows.Controls;
using AppWpf.Produtos;
using Microsoft.Extensions.DependencyInjection;
using Window = HandyControl.Controls.Window;

namespace AppWpf
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private void ListarProdutos_Click(object sender, RoutedEventArgs e)
        {
            AbrirConteudo(_serviceProvider.GetService<ListarProdutosContent>());
        }

        private void NovoProduto_Click(object sender, RoutedEventArgs e)
        {
            AbrirConteudo(_serviceProvider.GetService<NovoProdutoContent>());
        }

        public void AbrirConteudo(UserControl userControl)
        {
            Conteudo.Children.Clear();
            Conteudo.Children.Add(userControl);
        }
    }
}
