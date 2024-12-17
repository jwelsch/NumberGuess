using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System;

namespace NumberGuess;

public class DigitControl : TemplatedControl
{
    /// <summary>
    /// Defines the <see cref="Digit"/> property.
    /// </summary>
    public static readonly StyledProperty<char?> DigitProperty =
        AvaloniaProperty.Register<DigitControl, char?>(
            nameof(Digit));

    /// <summary>
    /// Gets or sets the Digit.
    /// </summary>
    public char? Digit
    {
        get => GetValue(DigitProperty);
        set => SetValue(DigitProperty, value);
    }

    /// <summary>
    /// Defines the <see cref="FixedWidth"/> property.
    /// </summary>
    public static readonly StyledProperty<int> FixedWidthProperty =
        AvaloniaProperty.Register<DigitControl, int>(
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
        AvaloniaProperty.Register<DigitControl, bool>(
            nameof(HighlightBorder));

    /// <summary>
    /// Gets or sets the HighlightBorder.
    /// </summary>
    public bool HighlightBorder
    {
        get => GetValue(HighlightBorderProperty);
        set => SetValue(HighlightBorderProperty, value);
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