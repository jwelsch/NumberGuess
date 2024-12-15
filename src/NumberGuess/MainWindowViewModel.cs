using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NumberGuess
{
    internal partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IAvaloniaKeyToCharConverter _avaloniaKeyToCharConverter;
        private readonly IDigitKeyDetector _digitKeyDetector;

        [ObservableProperty]
        private string? _digitOne;

        [ObservableProperty]
        private string? _digitTwo;

        [ObservableProperty]
        private string? _digitThree;

        [ObservableProperty]
        private string? _digitFour;

        public MainWindowViewModel(IAvaloniaKeyToCharConverter avaloniaKeyToCharConverter, IDigitKeyDetector digitKeyDetector)
        {
            _avaloniaKeyToCharConverter = avaloniaKeyToCharConverter;
            _digitKeyDetector = digitKeyDetector;
        }

        public bool ProcessKey(Key key)
        {
            if (!_digitKeyDetector.IsDigitKey(key))
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
