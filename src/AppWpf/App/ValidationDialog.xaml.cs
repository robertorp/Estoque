using System.Windows.Controls;
using Estoque.Core.Messages;

namespace AppWpf
{
    public partial class ValidationDialog : UserControl
    {
        public ValidationDialog(GenericResponse genericResponse)
        {
            InitializeComponent();

            Validacoes.Text = string.Empty;

            foreach (var validationResultError in genericResponse.ValidationResult.Errors)
            {
                Validacoes.Text += $"{validationResultError.ErrorMessage}\n";
            }
        }
    }
}
