using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Globalization;

namespace NumberGuess
{
    public class StateToForegroundConverter : IValueConverter
    {
        private readonly IResourceHost? _resourceHost;
        private readonly ThemeVariant? _themeVariant;
        private SolidColorBrush? _defaultBrush;
        private SolidColorBrush? _inputBrush;

        public StateToForegroundConverter(IAppResourceHostProvider appResourceHostProvider)
        {
            _resourceHost = appResourceHostProvider.GetResourceHost();
            _themeVariant = appResourceHostProvider.GetThemeVariant();
        }

        public StateToForegroundConverter()
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

            if (state == CharacterState.Input)
            {
                if (_inputBrush == null)
                {
                    if (!(_resourceHost?.TryFindResource("NumberGuessSelection", _themeVariant, out object? resource) ?? false)
                        || resource == null
                        || resource is not SolidColorBrush brush)
                    {
                        return null;
                    }

                    _inputBrush = brush;
                }

                return _inputBrush;
            }

            if (_defaultBrush == null)
            {
                if (!(_resourceHost?.TryFindResource("NumberGuessForeground", _themeVariant, out object? resource) ?? false)
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
