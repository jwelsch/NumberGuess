﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberGuess
{
    public enum NumberGuessGameState
    {
        Uninitialized,
        Playing,
        Won,
        Lost
    }

    public enum DigitInputState
    {
        Input,
        Correct,
        WrongDigit,
        WrongPlacement
    }


    public record DigitInputResult(DigitInputState State, char? Input = null);

    public record AttemptResult(AttemptState State, IReadOnlyList<DigitInputResult> DigitInputResult);

    public interface INumberGuessGameTracker
    {
        NumberGuessGameState State { get; }

        IReadOnlyList<char> Answer { get; }

        int AttemptCount { get; }

        bool IsPlaying { get; }

        bool IsComplete { get; }

        bool CanInput { get; }

        bool CanSubmit { get; }

        int DigitPlace { get; }

        IReadOnlyList<AttemptResult> AttemptResults { get; }

        void Start(char[] answer, int attemptCount);

        void Input(char input);

        void Back();

        void Submit();

        event EventHandler<EventArgs>? GameStateChanged;
    }

    public class NumberGuessGameTracker : INumberGuessGameTracker
    {
        public const int AttemptCountMax = 10;

        private AttemptTracker? _attemptTracker;
        private List<AttemptResult> _attemptResults = new();

        public event EventHandler<EventArgs>? GameStateChanged;

        private NumberGuessGameState _state;
        public NumberGuessGameState State
        {
            get => _state;
            private set
            {
                if (_state != value)
                {
                    _state = value;
                    GameStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public IReadOnlyList<char> Answer { get; private set; } = new char[0];

        public int AttemptCount { get; private set; }

        public bool IsPlaying => State == NumberGuessGameState.Playing;

        public bool IsComplete => State == NumberGuessGameState.Won || State == NumberGuessGameState.Lost;

        public bool CanInput => IsPlaying && (_attemptTracker?.CanInput ?? false);

        public bool CanSubmit => IsPlaying && (_attemptTracker?.CanSubmit ?? false);

        public int DigitPlace => _attemptTracker?.DigitPlace ?? -1;

        public IReadOnlyList<AttemptResult> AttemptResults => _attemptResults;

        public int AttemptsRemaining => AttemptCount - _attemptResults.Count;

        public void Start(char[] answer, int attemptCount)
        {
            if (answer.Length <= 0 || answer.Length > AttemptTracker.MaxDigitCount)
            {
                throw new ArgumentException($"The answer length must be greater than or equal to 1 and less than or equal to {AttemptTracker.MaxDigitCount}.", nameof(answer));
            }

            if (attemptCount <= 0 || attemptCount > AttemptCountMax)
            {
                throw new ArgumentException($"The attempt count must be greater than or equal to 1 and less than or equal to {AttemptCountMax}.", nameof(attemptCount));
            }

            State = NumberGuessGameState.Playing;

            Answer = answer;
            AttemptCount = attemptCount;

            _attemptTracker = new AttemptTracker();
            _attemptTracker.Start(answer.Length);

            _attemptResults.Clear();
        }

        public void Input(char input)
        {
            if (!IsPlaying)
            {
                throw new InvalidOperationException($"Expected state to be {NumberGuessGameState.Playing}, but it was {State}.");
            }

            _attemptTracker?.SetInput(input);
        }

        public void Back()
        {
            if (!IsPlaying)
            {
                throw new InvalidOperationException($"Expected state to be {NumberGuessGameState.Playing}, but it was {State}.");
            }

            _attemptTracker?.Back();
        }

        public void Submit()
        {
            if (!IsPlaying)
            {
                throw new InvalidOperationException($"Expected state to be {NumberGuessGameState.Playing}, but it was {State}.");
            }

            if (!CanSubmit)
            {
                throw new InvalidOperationException($"Must input all digits before submitting. '{DigitPlace}' out of '{Answer.Count}' digits have been submitted.");
            }

            if (CheckAttempt())
            {
                State = NumberGuessGameState.Won;
            }
            else
            {
                if (AttemptsRemaining <= 0)
                {
                    State = NumberGuessGameState.Lost;
                }
                else
                {
                    _attemptTracker = new AttemptTracker();
                    _attemptTracker.Start(Answer.Count);
                }
            }
        }

        private bool CheckAttempt()
        {
            if (_attemptTracker == null)
            {
                return false;
            }

            var inputs = new List<DigitInputResult>();

            var wasCorrect = true;

            for (var i = 0; i < Answer.Count; i++)
            {
                var input = _attemptTracker.Inputs[i];

                if (Answer[i] == input)
                {
                    inputs.Add(new DigitInputResult(DigitInputState.Correct, input));
                }
                else
                {
                    wasCorrect = false;

                    var wrongPlacement = false;

                    if (input != null)
                    {
                        wrongPlacement = Answer.Contains(input.Value);
                    }

                    inputs.Add(new DigitInputResult(wrongPlacement ? DigitInputState.WrongPlacement : DigitInputState.WrongDigit, input));
                }
            }

            _attemptResults.Add(new AttemptResult(wasCorrect ? AttemptState.Won : AttemptState.Lost, inputs));

            return wasCorrect;
        }
    }
}
