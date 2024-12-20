using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
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
    /// Defines the <see cref="State"/> property.
    /// </summary>
    public static readonly DirectProperty<DigitControl, CharacterState> StateProperty =
        AvaloniaProperty.RegisterDirect<DigitControl, CharacterState>(
            nameof(State),             // The name of the property.
            o => o.State,              // The getter of the property.
            (o, v) => o.State = v);    // The setter of the property.

    // State backing field.
    private CharacterState _state;

    /// <summary>
    /// Gets or sets the State.
    /// </summary>
    public CharacterState State
    {
        get => _state;
        private set => SetAndRaise(StateProperty, ref _state, value);
    }

    private IBrush? _borderBrush;
    private IBrush? _highlightBorderBrush;
    private IBrush? _backgroundBrush;
    private Border? _textBorder;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _borderBrush = this.FindResourceExt<SolidColorBrush>(ActualThemeVariant, "NumberGuessBorderMedium");
        _highlightBorderBrush = this.FindResourceExt<SolidColorBrush>(ActualThemeVariant, "NumberGuessSelection");
        _backgroundBrush = this.FindResourceExt<SolidColorBrush>(ActualThemeVariant, "NumberGuessBackground");

        _textBorder = e.NameScope.Find<Border>(nameof(_textBorder)) ?? throw new Exception($"Could not find control '{nameof(_textBorder)}'.");
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property.Name == nameof(State) && _textBorder != null)
        {
            if (_textBorder != null)
            {
                _textBorder.BorderBrush = State == CharacterState.Input ? _highlightBorderBrush : _borderBrush;

                _textBorder.Background = State switch
                {
                    CharacterState.WrongCharacter => Brushes.IndianRed,
                    CharacterState.WrongPlacement => Brushes.LightYellow,
                    CharacterState.Input => Brushes.LightGreen,
                    _ => _backgroundBrush
                };
            }
        }
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
    }
}