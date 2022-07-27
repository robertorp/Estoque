using System.Windows.Controls;

namespace AppWpf.Produtos
{ 
    public partial class ListarProdutosContent : UserControl
    {
        private readonly ListarProdutosContext _context;

        public ListarProdutosContent(ListarProdutosContext context)
        {
            InitializeComponent();
            _context = context;
            DataContext = context;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _context.Inicializando();
        }
    }
}
