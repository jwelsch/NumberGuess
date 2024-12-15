using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;

namespace NumberGuess
{
    internal partial class MainWindowViewModel : ViewModelBase
    {
        private readonly Key[] DigitKeys = new[]
        {
            Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,
            Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9
        };

        private readonly IAvaloniaKeyToCharConverter _avaloniaKeyToCharConverter;

        [ObservableProperty]
        private string? _digitOne;

        [ObservableProperty]
        private string? _digitTwo;

        [ObservableProperty]
        private string? _digitThree;

        [ObservableProperty]
        private string? _digitFour;

        public MainWindowViewModel(IAvaloniaKeyToCharConverter avaloniaKeyToCharConverter)
        {
            _avaloniaKeyToCharConverter = avaloniaKeyToCharConverter;
        }

        public bool ProcessKey(Key key)
        {
            if (!DigitKeys.Contains(key))
            {
                return false;
            }

            if (DigitOne == null)
            {
                DigitOne = _avaloniaKeyToCharConverter.GetKeyChar(key).ToString();
            }
            else if (DigitTwo == null)
            {
                DigitTwo = _avaloniaKeyToCharConverter.GetKeyChar(key).ToString();
            }
            else if (DigitThree == null)
            {
                DigitThree = _avaloniaKeyToCharConverter.GetKeyChar(key).ToString();
            }
            else if (DigitFour == null)
            {
                DigitFour = _avaloniaKeyToCharConverter.GetKeyChar(key).ToString();
            }

            return true;
        }
    }
}
