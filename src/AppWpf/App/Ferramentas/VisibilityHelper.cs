using System.Windows;
using System.Windows.Controls;

namespace AppWpf.Ferramentas;

public static class VisibilityHelper
{
    public static readonly DependencyProperty IsVisibleProperty
        = DependencyProperty.RegisterAttached(
            "IsVisible",
            typeof(bool?),
            typeof(VisibilityHelper),
            new FrameworkPropertyMetadata(default(bool?),
                                          FrameworkPropertyMetadataOptions.AffectsArrange |
                                          FrameworkPropertyMetadataOptions.AffectsMeasure |
                                          FrameworkPropertyMetadataOptions.AffectsRender,
                                          (d, e) => SetVisibility(d, (e.NewValue as bool?).GetValueOrDefault() ? Visibility.Visible : Visibility.Collapsed)));

    public static void SetIsVisible(DependencyObject element, bool? value)
    {
        element.SetValue(IsVisibleProperty, BooleanBoxes.Box(value));
    }

    public static bool? GetIsVisible(DependencyObject element)
    {
        return (bool?)element.GetValue(IsVisibleProperty);
    }

    public static readonly DependencyProperty IsCollapsedProperty
        = DependencyProperty.RegisterAttached(
            "IsCollapsed",
            typeof(bool?),
            typeof(VisibilityHelper),
            new FrameworkPropertyMetadata(default(bool?),
                                          FrameworkPropertyMetadataOptions.AffectsArrange |
                                          FrameworkPropertyMetadataOptions.AffectsMeasure |
                                          FrameworkPropertyMetadataOptions.AffectsRender,
                                          (d, e) => SetVisibility(d, (e.NewValue as bool?).GetValueOrDefault() ? Visibility.Collapsed : Visibility.Visible)));

    public static void SetIsCollapsed(DependencyObject element, bool? value)
    {
        element.SetValue(IsCollapsedProperty, BooleanBoxes.Box(value));
    }

    public static bool? GetIsCollapsed(DependencyObject element)
    {
        return (bool?)element.GetValue(IsCollapsedProperty);
    }

    public static readonly DependencyProperty IsHiddenProperty
        = DependencyProperty.RegisterAttached(
            "IsHidden",
            typeof(bool?),
            typeof(VisibilityHelper),
            new FrameworkPropertyMetadata(default(bool?),
                                          FrameworkPropertyMetadataOptions.AffectsArrange |
                                          FrameworkPropertyMetadataOptions.AffectsMeasure |
                                          FrameworkPropertyMetadataOptions.AffectsRender,
                                          (d, e) => SetVisibility(d, (e.NewValue as bool?).GetValueOrDefault() ? Visibility.Hidden : Visibility.Visible)));

    public static void SetIsHidden(DependencyObject element, bool? value)
    {
        element.SetValue(IsHiddenProperty, BooleanBoxes.Box(value));
    }

    public static bool? GetIsHidden(DependencyObject element)
    {
        return (bool?)element.GetValue(IsHiddenProperty);
    }

    private static void SetVisibility(DependencyObject depObject, Visibility visibility)
    {
        switch (depObject)
        {
            case FrameworkElement fe:
                fe.Visibility = visibility;
                break;
            case DataGridColumn dataGridColumn:
                dataGridColumn.Visibility = visibility;
                break;
        }
    }
}