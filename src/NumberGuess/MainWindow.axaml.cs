using Avalonia.Controls;
using Avalonia.Input;

namespace NumberGuess
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            KeyDownEvent.AddClassHandler<TopLevel>(OnKeyDownHandler);
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