using CommunityToolkit.Mvvm.ComponentModel;

namespace NumberGuess
{
    public enum CharacterState
    {
        Default,
        Input,
        Correct,
        WrongCharacter,
        WrongPlacement
    }

    public partial class CharacterViewModel : ViewModelBase
    {
        [ObservableProperty]
        private char? _char;

        [ObservableProperty]
        private bool _highlight;

        [ObservableProperty]
        private CharacterState _state = CharacterState.Default;
    }
}
