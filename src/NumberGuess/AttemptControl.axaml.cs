using Avalonia;
using Avalonia.Controls.Primitives;

namespace NumberGuess;

public class AttemptControl : TemplatedControl
{
    /// <summary>
    /// Defines the <see cref="CharacterPlaces"/> property.
    /// </summary>
    public static readonly StyledProperty<int> CharacterPlacesProperty =
        AvaloniaProperty.Register<AttemptControl, int>(
            nameof(CharacterPlaces));

    /// <summary>
    /// Gets or sets the CharacterPlaces.
    /// </summary>
    public int CharacterPlaces
    {
        get => GetValue(CharacterPlacesProperty);
        set => SetValue(CharacterPlacesProperty, value);
    }
}