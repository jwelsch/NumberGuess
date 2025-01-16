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
            services.AddTransient<IAttemptTracker, AttemptTracker>();
            services.AddTransient<INumberGuessGameTrackerFactory, NumberGuessGameTrackerFactory>();
            services.AddTransient<IAnswerGenerator, AnswerGenerator>();
            services.AddSingleton<IAppResourceHostProvider>(sp => new AppResourceHostProvider((NumberGuess.App)(App.Current ?? throw new System.Exception($"App.Current was null."))));
        }
    }
}
