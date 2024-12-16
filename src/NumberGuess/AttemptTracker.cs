using System;
using System.Collections.Generic;

namespace NumberGuess
{
    public enum DigitInputState
    {
        Input,
        Correct,
        WrongDigit,
        WrongPlacement
    }

    public enum AttemptState
    {
        Uninitialized,
        Input,
        Submit,
        Won,
        Lost
    }

    public interface IAttemptTracker
    {
        AttemptState State { get; }

        int DigitCount { get; }

        int DigitPlace { get; }

        bool CanInput { get; }

        bool CanSubmit { get; }

        IReadOnlyList<char?> Inputs { get; }

        void Start(int digitCount);

        void SetInput(char input);

        void Back();
    }

    public record DigitInputInfo(DigitInputState State, int DigitPlace);

    public class AttemptTracker : IAttemptTracker
    {
        private char?[] _inputs = new char?[0];

        public AttemptState State { get; private set; }

        public int DigitCount { get; private set; } = 0;

        public int DigitPlace { get; private set; } = 0;

        public IReadOnlyList<char?> Inputs => _inputs;

        public bool CanInput => State == AttemptState.Input;

        public bool CanSubmit => State == AttemptState.Submit;

        public void Start(int digitCount)
        {
            DigitCount = digitCount;
            _inputs = new char?[DigitCount];
            State = AttemptState.Input;
        }

        public void SetInput(char input)
        {
            if (State != AttemptState.Input)
            {
                throw new InvalidOperationException($"Expected the AttemptState to be {AttemptState.Input}, but it was {State}.");
            }

            if (DigitPlace >= DigitCount)
            {
                throw new InvalidOperationException($"The AttemptState was {State}, but the DigitPlace ({DigitPlace}) was greater than or equal to the DigitCount ({DigitCount}).");
            }

            _inputs[DigitPlace] = input;

            var newDigitPlace = DigitPlace + 1;

            if (newDigitPlace >= DigitCount)
            {
                State = AttemptState.Submit;
                DigitPlace = DigitCount;
            }
            else
            {
                State = AttemptState.Input;
                DigitPlace = newDigitPlace;
            }
        }

        public void Back()
        {
            if (State == AttemptState.Submit)
            {
                DigitPlace = DigitCount - 1;
                State = AttemptState.Input;
            }
            else if (State == AttemptState.Input)
            {
                DigitPlace = DigitPlace > 0 ? DigitPlace - 1 : 0;
            }
            else
            {
                throw new InvalidOperationException($"Expected the AttemptState to be {AttemptState.Input} or {AttemptState.Submit}, but it was {State}.");
            }

            _inputs[DigitPlace] = null;
        }
    }
}
