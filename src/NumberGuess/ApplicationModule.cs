using Microsoft.Extensions.DependencyInjection;
using NumberGuess.Common.DependencyInjection;

namespace NumberGuess
{
    internal class ApplicationModule : Module
    {
        protected override void Load(IServiceCollection services, object[]? parameters = null)
        {
            services.AddTransient<MainWindowViewModel>();

            services.AddTransient<IAvaloniaKeyToCharConverter, AvaloniaKeyToCharConverter>();
            services.AddTransient<IDigitKeyDetector, DigitKeyDetector>();
        }
    }
}
