namespace AppWpf
{
    public partial class TextDialog
    {
        public TextDialog(string mensagem)
        {
            InitializeComponent();
            Menssagem.Text = string.Empty;
            Menssagem.Text = mensagem;
        }
    }
}
