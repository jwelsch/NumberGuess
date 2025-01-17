using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Globalization;

namespace NumberGuess
{
    public class StateToBackgroundConverter : IValueConverter
    {
        private readonly IResourceHost? _resourceHost;
        private readonly ThemeVariant? _themeVariant;
        private SolidColorBrush? _defaultBrush;
        private SolidColorBrush? _wrongPlacementBrush;
        private SolidColorBrush? _correctBrush;

        public StateToBackgroundConverter(IAppResourceHostProvider appResourceHostProvider)
        {
            _resourceHost = appResourceHostProvider.GetResourceHost();
            _themeVariant = appResourceHostProvider.GetThemeVariant();
        }

        public StateToBackgroundConverter()
        {
            _resourceHost = App.Current;
            _themeVariant = App.Current?.ActualThemeVariant;
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null
                || value is not CharacterState state)
            {
                return null;
            }

            if (state == CharacterState.WrongPlacement)
            {
                if (_wrongPlacementBrush == null)
                {
                    if (!(_resourceHost?.TryFindResource("NumberGuessDigitWrongPlacement", _themeVariant, out object? resource) ?? false)
                        || resource == null
                        || resource is not SolidColorBrush brush)
                    {
                        return null;
                    }

                    _wrongPlacementBrush = brush;
                }

                return _wrongPlacementBrush;
            }
            else if (state == CharacterState.Correct)
            {
                if (_correctBrush == null)
                {
                    if (!(_resourceHost?.TryFindResource("NumberGuessDigitCorrect", _themeVariant, out object? resource) ?? false)
                        || resource == null
                        || resource is not SolidColorBrush brush)
                    {
                        return null;
                    }

                    _correctBrush = brush;
                }

                return _correctBrush;
            }

            if (_defaultBrush == null)
            {
                if (!(_resourceHost?.TryFindResource("NumberGuessBackground", _themeVariant, out object? resource) ?? false)
                    || resource == null
                    || resource is not SolidColorBrush brush)
                {
                    return null;
                }

                _defaultBrush = brush;
            }

            return _defaultBrush;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
