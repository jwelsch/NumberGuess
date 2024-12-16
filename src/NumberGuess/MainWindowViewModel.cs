using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace NumberGuess
{
    internal partial class MainWindowViewModel : ViewModelBase
    {
        private const int DigitCount = 4;
        private const int AttemptCount = 5;

        private readonly IAvaloniaKeyToCharConverter _avaloniaKeyToCharConverter;
        private readonly IDigitKeyDetector _digitKeyDetector;
        private readonly INumberGuessGameTrackerFactory _gameTrackerFactory;
        private readonly IAnswerGenerator _answerGenerator;

        private INumberGuessGameTracker _gameTracker;

        [ObservableProperty]
        private string? _digitOne;

        [ObservableProperty]
        private string? _digitTwo;

        [ObservableProperty]
        private string? _digitThree;

        [ObservableProperty]
        private string? _digitFour;

        [ObservableProperty]
        private int _digitPlace;

        [ObservableProperty]
        private bool _canSubmit;

        [ObservableProperty]
        private string? _messageText;

        public MainWindowViewModel(IAvaloniaKeyToCharConverter avaloniaKeyToCharConverter, IDigitKeyDetector digitKeyDetector, INumberGuessGameTrackerFactory gameTrackerFactory, IAnswerGenerator answerGenerator)
        {
            _avaloniaKeyToCharConverter = avaloniaKeyToCharConverter;
            _digitKeyDetector = digitKeyDetector;
            _gameTrackerFactory = gameTrackerFactory;
            _answerGenerator = answerGenerator;
            _gameTracker = _gameTrackerFactory.Create();

            DigitPlace = -1;

            InitializeGame();
        }

        private void InitializeGame()
        {
            try
            {
                DigitOne = null;
                DigitTwo = null;
                DigitThree = null;
                DigitFour = null;
                MessageText = null;

                var answer = _answerGenerator.Generate(AnswerCharSet.Digits, DigitCount);
                _gameTracker.Start(answer.ToCharArray(), AttemptCount);

                DigitPlace = _gameTracker.DigitPlace;
                CanSubmit = _gameTracker.CanSubmit;

                System.Diagnostics.Trace.WriteLine($"Answer: {answer}");
            }
            catch (Exception ex)
            {
                MessageText = $"Failed to initialize the game.\n{ex}";
                System.Diagnostics.Trace.WriteLine(MessageText);
            }
        }

        public void ProcessKey(Key key)
        {
            if (_digitKeyDetector.IsDigitKey(key) && _gameTracker.CanInput)
            {
                ProcessDigitKey(key);
            }
            else if (key == Key.Enter && _gameTracker.CanSubmit)
            {
                Submit();
            }
        }

        private void ProcessDigitKey(Key key)
        {
            try
            {
                var c = _avaloniaKeyToCharConverter.GetKeyChar(key);

                SetDigitInput(c.ToString());

                _gameTracker.Input(c);

                DigitPlace = _gameTracker.DigitPlace;
                CanSubmit = _gameTracker.CanSubmit;
            }
            catch (Exception ex)
            {
                MessageText = $"Error while processing digit key.\n{ex}";
                System.Diagnostics.Trace.WriteLine(MessageText);
            }
        }

        private void SetDigitInput(string input)
        {
            if (_gameTracker.DigitPlace == 0)
            {
                DigitOne = input;
            }
            else if (_gameTracker.DigitPlace == 1)
            {
                DigitTwo = input;
            }
            else if (_gameTracker.DigitPlace == 2)
            {
                DigitThree = input;
            }
            else if (_gameTracker.DigitPlace == 3)
            {
                DigitFour = input;
            }
            else
            {
                throw new InvalidOperationException($"The game tracker digit place was not an expected value: {_gameTracker.DigitPlace}");
            }
        }

        [RelayCommand]
        public void Submit()
        {
            try
            {
                _gameTracker.Submit();
                CanSubmit = _gameTracker.CanSubmit;
                DigitPlace = DigitCount;

                if (_gameTracker.State == NumberGuessGameState.Won)
                {
                    MessageText = "You won!";
                }
                else if (_gameTracker.State == NumberGuessGameState.Lost)
                {
                    MessageText = "You lost!";
                }
                else
                {
                    MessageText = $"Unknown game tracker state: '{_gameTracker.State}'.";
                }
            }
            catch (Exception ex)
            {
                MessageText = $"Error while submitting attempt.\n{ex}";
                System.Diagnostics.Trace.WriteLine(MessageText);
            }
        }

        [RelayCommand]
        public void ShowStats()
        {

        }

        [RelayCommand]
        public void Reset()
        {
            InitializeGame();
        }
    }
}
