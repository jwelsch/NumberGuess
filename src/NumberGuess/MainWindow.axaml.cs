using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Globalization;

namespace NumberGuess
{


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            KeyDownEvent.AddClassHandler<TopLevel>(OnKeyDownHandler);
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
        }

        private void OnKeyDownHandler(object? sender, KeyEventArgs e)
        {
            if (DataContext == null
                || DataContext is not MainWindowViewModel model)
            {
                return;
            }

            model.ProcessKey(e.Key);
        }
    }
}