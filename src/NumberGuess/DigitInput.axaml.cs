using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace NumberGuess;

public class BorderThicknessConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null || value is not bool highlight || !highlight)
        {
            return new Thickness(1);
        }

        return new Thickness(3);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}

public class DigitInput : TemplatedControl
{
    /// <summary>
    /// Defines the <see cref="Digit"/> property.
    /// </summary>
    public static readonly StyledProperty<string?> DigitProperty =
        AvaloniaProperty.Register<DigitInput, string?>(
            nameof(Digit));

    /// <summary>
    /// Gets or sets the Digit.
    /// </summary>
    public string? Digit
    {
        get => GetValue(DigitProperty);
        set => SetValue(DigitProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="FixedWidth"/> property.
    /// </summary>
    public static readonly StyledProperty<int> FixedWidthProperty =
        AvaloniaProperty.Register<DigitInput, int>(
            nameof(FixedWidth));

    /// <summary>
    /// Gets or sets the FixedWidth.
    /// </summary>
    public int FixedWidth
    {
        get => GetValue(FixedWidthProperty);
        set => SetValue(FixedWidthProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="HighlightBorder"/> property.
    /// </summary>
    public static readonly StyledProperty<bool> HighlightBorderProperty =
        AvaloniaProperty.Register<DigitInput, bool>(
            nameof(HighlightBorder));

    /// <summary>
    /// Gets or sets the HighlightBorder.
    /// </summary>
    public bool HighlightBorder
    {
        get => GetValue(HighlightBorderProperty);
        set => SetValue(HighlightBorderProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="BackgroundColor"/> property.
    /// </summary>
    public static readonly StyledProperty<Brush?> BackgroundColorProperty =
        AvaloniaProperty.Register<DigitInput, Brush?>(
            nameof(BackgroundColor));

    /// <summary>
    /// Gets or sets the BackgroundColor.
    /// </summary>
    public Brush? BackgroundColor
    {
        get => GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    private SolidColorBrush? _borderBrush;
    private SolidColorBrush? _highlightBorderBrush;
    private Border? _textBorder;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _borderBrush = this.FindResourceExt<SolidColorBrush>(ActualThemeVariant, "NumberGuessBorderMedium");
        _highlightBorderBrush = this.FindResourceExt<SolidColorBrush>(ActualThemeVariant, "NumberGuessSelection");

        _textBorder = e.NameScope.Find<Border>(nameof(_textBorder)) ?? throw new Exception($"Could not find control '{nameof(_textBorder)}'.");
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property.Name == nameof(HighlightBorder) && _textBorder != null)
        {
            _textBorder.BorderBrush = HighlightBorder ? _highlightBorderBrush : _borderBrush;
        }
    }
}