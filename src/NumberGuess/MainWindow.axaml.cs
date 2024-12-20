using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Globalization;

namespace NumberGuess
{
    public class StatusToBackgroundConverter : IValueConverter
    {
        private readonly IResourceHost? _resourceHost;
        private readonly ThemeVariant? _themeVariant;
        private SolidColorBrush? _defaultBrush;
        private SolidColorBrush? _inputBrush;

        public StatusToBackgroundConverter(IAppResourceHostProvider appResourceHostProvider)
        {
            _resourceHost = appResourceHostProvider.GetResourceHost();
            _themeVariant = appResourceHostProvider.GetThemeVariant();
        }

        public StatusToBackgroundConverter()
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

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            KeyDownEvent.AddClassHandler<TopLevel>(OnKeyDownHandler);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            //_oneDigitInput.HighlightBorder = false;
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);

            //_oneDigitInput.HighlightBorder = true;
        }

        private void OnKeyDownHandler(object? sender, KeyEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine($"MainWindow.OnKeyDownHandler - e.Key: {e.Key}");

            if (DataContext == null
                || DataContext is not MainWindowViewModel model)
            {
                return;
            }

            model.ProcessKey(e.Key);
        }
    }
}