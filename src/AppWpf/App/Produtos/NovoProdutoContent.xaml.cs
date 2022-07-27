using System.Windows.Controls;
using Estoque.Produto.Api.Application;

namespace AppWpf.Produtos
{
    public partial class NovoProdutoContent : UserControl
    {
        private NovoProdutoContext _context;

        public NovoProdutoContent(NovoProdutoContext context)
        {
            InitializeComponent();
            _context = context;
            DataContext = _context;
        }

        public void AlterarProduto(ProdutoViewModel produtoViewModel)
        {
            _context.AlterarProduto(produtoViewModel);
        }
    }
}
