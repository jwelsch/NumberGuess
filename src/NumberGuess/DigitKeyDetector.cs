using Avalonia.Input;
using System;
using System.Linq;

namespace NumberGuess
{
    public interface IDigitKeyDetector
    {
        bool IsDigitKey(Key key);
    }

    public class DigitKeyDetector : IDigitKeyDetector
    {
        private static readonly Key[] DigitKeys = new[]
        {
            Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,
            Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9
        };

        public bool IsDigitKey(Key key) => DigitKeys.Contains(key);
    }
}
