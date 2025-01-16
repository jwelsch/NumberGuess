using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace NumberGuess
{
    internal partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IAvaloniaKeyToCharConverter _avaloniaKeyToCharConverter;
        private readonly IDigitKeyDetector _digitKeyDetector;
        private readonly INumberGuessGameTrackerFactory _gameTrackerFactory;
        private readonly IAnswerGenerator _answerGenerator;

        private INumberGuessGameTracker _gameTracker;

        [ObservableProperty]
        private int _digitCount;

        [ObservableProperty]
        private int _attemptCount;

        [ObservableProperty]
        private bool _canSubmit;

        [ObservableProperty]
        private string? _messageText;

        [ObservableProperty]
        private ObservableCollection<ObservableCollection<CharacterViewModel>> _guessedCharacters = new();

        [ObservableProperty]
        private List<CharacterViewModel> _inputCharacters = new();

        public MainWindowViewModel(IAvaloniaKeyToCharConverter avaloniaKeyToCharConverter, IDigitKeyDetector digitKeyDetector, INumberGuessGameTrackerFactory gameTrackerFactory, IAnswerGenerator answerGenerator)
        {
            _avaloniaKeyToCharConverter = avaloniaKeyToCharConverter;
            _digitKeyDetector = digitKeyDetector;
            _gameTrackerFactory = gameTrackerFactory;
            _answerGenerator = answerGenerator;
            _gameTracker = _gameTrackerFactory.Create();

            GuessedCharacters.CollectionChanged += GuessedCharacters_CollectionChanged;

            DigitCount = 4;
            AttemptCount = 5;

            InitializeGame();
        }

        private void GuessedCharacters_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(DigitCount))
            {
                InputCharacters.Clear();

                for (var i = 0; i < DigitCount; i++)
                {
                    InputCharacters.Add(new CharacterViewModel
                    {
                        Char = ' ',
                        State = CharacterState.Default
                    });
                }
            }
        }

        private void InitializeGame()
        {
            try
            {
                if (GuessedCharacters.Count > 0)
                {
                    GuessedCharacters.Clear();
                }

                var answer = _answerGenerator.Generate(AnswerCharSet.Digits, DigitCount);

                _gameTracker.Start(answer.ToCharArray(), AttemptCount);

                MessageText = null;
                CanSubmit = _gameTracker.CanSubmit;

                ResetInputCharacters();

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
            else if (key == Key.Back || key == Key.Delete)
            {
                ProcessBack();
            }
        }

        private void ProcessDigitKey(Key key)
        {
            try
            {
                InputCharacters[_gameTracker.DigitPlace].State = CharacterState.Default;

                var c = _avaloniaKeyToCharConverter.GetKeyChar(key);

                SetDigitInput(c.ToString());

                _gameTracker.Input(c);

                CanSubmit = _gameTracker.CanSubmit;

                if (_gameTracker.DigitPlace >= 0 && _gameTracker.DigitPlace < InputCharacters.Count)
                {
                    InputCharacters[_gameTracker.DigitPlace].State = CharacterState.Input;
                }
            }
            catch (Exception ex)
            {
                MessageText = $"Error while processing digit key.\n{ex}";
                System.Diagnostics.Trace.WriteLine(MessageText);
            }
        }

        private void SetDigitInput(string input)
        {
            if (_gameTracker.DigitPlace >= InputCharacters.Count)
            {
                throw new InvalidOperationException($"Game tracker digit place '{_gameTracker.DigitPlace}' is greater than or equal to the character count '{GuessedCharacters.Count}'.");
            }

            InputCharacters[_gameTracker.DigitPlace].Char = input[0];
        }

        private void ProcessBack()
        {
            try
            {
                var index = _gameTracker.DigitPlace >= DigitCount ? DigitCount - 1 : _gameTracker.DigitPlace;

                if (index < 0)
                {
                    return;
                }

                InputCharacters[index].Char = ' ';
                InputCharacters[index].State = CharacterState.Default;

                _gameTracker.Back();
                CanSubmit = _gameTracker.CanSubmit;

                if (_gameTracker.DigitPlace >= 0 && _gameTracker.DigitPlace < InputCharacters.Count)
                {
                    InputCharacters[_gameTracker.DigitPlace].Char = ' ';
                    InputCharacters[_gameTracker.DigitPlace].State = CharacterState.Input;
                }
            }
            catch (Exception ex)
            {
                MessageText = $"Error while processing back key.\n{ex}";
                System.Diagnostics.Trace.WriteLine(MessageText);
            }
        }

        [RelayCommand]
        public void Submit()
        {
            try
            {
                _gameTracker.Submit();
                CanSubmit = _gameTracker.CanSubmit;

                if (_gameTracker.State == NumberGuessGameState.Won)
                {
                    MessageText = "You won!";
                }
                else if (_gameTracker.State == NumberGuessGameState.Lost)
                {
                    MessageText = "You lost!";
                }
                else if (_gameTracker.State == NumberGuessGameState.Playing)
                {
                    MoveInputToGuessed();
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

        private void MoveInputToGuessed()
        {
            var guessed = new ObservableCollection<CharacterViewModel>();

            var lastAttempt = _gameTracker.AttemptResults[^1];

            for (var i = 0; i < DigitCount; i++)
            {
                guessed.Add(new CharacterViewModel
                {
                    Char = lastAttempt.DigitInputResult[i].Input,
                    State = CharacterViewModel.ConvertFromDigitInputState(lastAttempt.DigitInputResult[i].State)
                });
            }

            GuessedCharacters.Add(guessed);

            ResetInputCharacters();
        }

        private void ResetInputCharacters()
        {
            for (var i = 0; i < InputCharacters.Count; i++)
            {
                InputCharacters[i].Char = ' ';
                InputCharacters[i].State = i == 0 ? CharacterState.Input : CharacterState.Default;
            }
        }
    }
}
