using System;
using System.Security.Cryptography;
using System.Text;

namespace NumberGuess
{
    [Flags]
    public enum AnswerCharSet
    {
        Digits = 0x1,
        Letters = 0x2,
    }

    public interface IAnswerGenerator
    {
        string Generate(AnswerCharSet charSet, int characterCount);
    }

    public class AnswerGenerator : IAnswerGenerator
    {
        private static readonly int _digitIndex = 0;
        private static readonly int _digitCount = 10;
        private static readonly int _letterIndex = 10;
        private static readonly int _letterCount = 26;

        private static readonly char[] _charSet = new[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        public string Generate(AnswerCharSet charSet, int characterCount)
        {
            if (characterCount <= 0)
            {
                throw new ArgumentException($"The character count must be greater than zero.", nameof(characterCount));
            }

            int start;
            int count;

            if (charSet == (AnswerCharSet.Digits | AnswerCharSet.Letters))
            {
                start = _digitIndex;
                count = _digitCount + _letterCount;
            }
            else if ((charSet & AnswerCharSet.Digits) == AnswerCharSet.Digits)
            {
                start = _digitIndex;
                count = _digitCount;
            }
            else if ((charSet & AnswerCharSet.Letters) == AnswerCharSet.Letters)
            {
                start = _letterIndex;
                count = _letterCount;
            }
            else
            {
                throw new ArgumentException($"Unknown value '{charSet}'.", nameof(charSet));
            }

            var rng = RandomNumberGenerator.Create();
            var randomNumbers = new byte[characterCount];

            rng.GetBytes(randomNumbers);

            var builder = new StringBuilder();

            for (var i = 0; i < randomNumbers.Length; i++)
            {
                var index = start + (randomNumbers[i] % count);
                builder.Append(_charSet[index]);
            }

            return builder.ToString();
        }
    }
}
