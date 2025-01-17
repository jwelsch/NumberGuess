using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Globalization;

namespace NumberGuess
{
    public class MessageColorConverter : IValueConverter
    {
        private readonly IResourceHost? _resourceHost;
        private readonly ThemeVariant? _themeVariant;
        private SolidColorBrush? _defaultBrush;

        public MessageColorConverter(IAppResourceHostProvider appResourceHostProvider)
        {
            _resourceHost = appResourceHostProvider.GetResourceHost();
            _themeVariant = appResourceHostProvider.GetThemeVariant();
        }

        public MessageColorConverter()
        {
            _resourceHost = App.Current;
            _themeVariant = App.Current?.ActualThemeVariant;
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null
                || value is not NumberGuessGameState state)
            {
                return null;
            }

            if (state == NumberGuessGameState.Won)
            {
                return Brushes.Green;
            }
            else if (state == NumberGuessGameState.Lost)
            {
                return Brushes.Red;
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
