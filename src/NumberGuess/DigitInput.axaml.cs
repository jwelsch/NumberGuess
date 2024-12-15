using Avalonia;
using Avalonia.Controls.Primitives;

namespace NumberGuess;

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
}