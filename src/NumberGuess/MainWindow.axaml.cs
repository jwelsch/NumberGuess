using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.Globalization;

namespace NumberGuess
{
    public class DigitPlaceHightlightConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || value is not int digitPlace
                || parameter == null || parameter is not string inputPlace)
            {
                return null;
            }

            return digitPlace == int.Parse(inputPlace);
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

            _oneDigitInput.HighlightBorder = false;
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);

            _oneDigitInput.HighlightBorder = true;
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