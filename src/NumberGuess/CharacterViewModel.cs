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
        private CharacterState _state = CharacterState.Default;

        public CharacterViewModel()
        {
        }

        public CharacterViewModel(CharacterViewModel model)
        {
            Char = model.Char;
            State = model.State;
        }

        public static CharacterState ConvertFromDigitInputState(DigitInputState digitInputState)
        {
            return digitInputState switch
            {
                DigitInputState.Input => CharacterState.Input,
                DigitInputState.Correct => CharacterState.Correct,
                DigitInputState.WrongDigit => CharacterState.WrongCharacter,
                DigitInputState.WrongPlacement => CharacterState.WrongPlacement,
                _ => CharacterState.Default
            };
        }
    }
}
