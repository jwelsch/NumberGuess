using Avalonia.Controls;
using Avalonia.Styling;

namespace NumberGuess
{
    public interface IAppResourceHostProvider
    {
        IResourceHost GetResourceHost();

        ThemeVariant GetThemeVariant();
    }

    public class AppResourceHostProvider : IAppResourceHostProvider
    {
        private readonly IResourceHost _resourceHost;
        private readonly ThemeVariant _themeVariant;

        public AppResourceHostProvider(App app)
        {
            _resourceHost = app;
            _themeVariant = app.ActualThemeVariant;
        }

        public IResourceHost GetResourceHost() => _resourceHost;

        public ThemeVariant GetThemeVariant() => _themeVariant;
    }
}
