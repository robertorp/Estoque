using HandyControl.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows;
using AppWpf.Ferramentas;

namespace AppWpf.Controles;

public class DecimalTextBox : TextBox
{
    private bool TemSeparador => Text.Contains(",");

    public static readonly DependencyProperty LimiteCasasDecimalProperty = DependencyProperty.Register("LimiteDecimal",
        typeof(int),
        typeof(DecimalTextBox),
        new PropertyMetadata(default(int)));

    public int LimiteDecimal
    {
        get => (int)GetValue(LimiteCasasDecimalProperty);
        set => SetValue(LimiteCasasDecimalProperty, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        FocusAdvancement.SetAdvancesByEnterKey(this, true);
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = true;
            return;
        }

        base.OnPreviewKeyDown(e);
    }

    protected override void OnPreviewTextInput(TextCompositionEventArgs e)
    {
        if (e.Text == "," && TemSeparador)
        {
            e.Handled = true;
            return;
        }

        if (AtingiuLimiteDecimals())
        {
            e.Handled = true;
            return;
        }

        e.Handled = !Regex.IsMatch(e.Text, "[0-9,]", RegexOptions.IgnoreCase);

        base.OnPreviewTextInput(e);
    }

    private bool AtingiuLimiteDecimals()
    {
        if (LimiteDecimal == default(int) || TemSeparador == false)
        {
            return false;
        }

        var textSplit = Text.Split(',');
        var posicaoSeparador = Text.IndexOf(',');

        return CaretIndex > posicaoSeparador && textSplit[1].Length >= LimiteDecimal;
    }
}