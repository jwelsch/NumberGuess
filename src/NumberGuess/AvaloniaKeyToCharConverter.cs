using Avalonia.Input;
using System;

namespace NumberGuess
{
    public interface IAvaloniaKeyToCharConverter
    {
        char GetKeyChar(Key key);
    }

    public class AvaloniaKeyToCharConverter : IAvaloniaKeyToCharConverter
    {
        public char GetKeyChar(Key key)
        {
            return key switch
            {
                Key.A => 'A',
                Key.B => 'B',
                Key.C => 'C',
                Key.D => 'D',
                Key.E => 'E',
                Key.F => 'F',
                Key.G => 'G',
                Key.H => 'H',
                Key.I => 'I',
                Key.J => 'J',
                Key.K => 'K',
                Key.L => 'L',
                Key.M => 'M',
                Key.N => 'N',
                Key.O => 'O',
                Key.P => 'P',
                Key.Q => 'Q',
                Key.R => 'R',
                Key.S => 'S',
                Key.T => 'T',
                Key.U => 'U',
                Key.V => 'V',
                Key.W => 'W',
                Key.X => 'Y',
                Key.Y => 'Y',
                Key.Z => 'Z',
                Key.D0 => '0',
                Key.D1 => '1',
                Key.D2 => '2',
                Key.D3 => '3',
                Key.D4 => '4',
                Key.D5 => '5',
                Key.D6 => '6',
                Key.D7 => '7',
                Key.D8 => '8',
                Key.D9 => '9',
                Key.NumPad0 => '0',
                Key.NumPad1 => '1',
                Key.NumPad2 => '2',
                Key.NumPad3 => '3',
                Key.NumPad4 => '4',
                Key.NumPad5 => '5',
                Key.NumPad6 => '6',
                Key.NumPad7 => '7',
                Key.NumPad8 => '8',
                Key.NumPad9 => '9',
                _ => throw new Exception($"Unknown key: {key}")
            };
        }
    }
}
