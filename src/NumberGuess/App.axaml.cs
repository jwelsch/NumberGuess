using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using NumberGuess.Common.DependencyInjection;
using System;

namespace NumberGuess
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var services = RegisterAppServices();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = services.GetRequiredService<MainWindowViewModel>()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private static IServiceProvider RegisterAppServices()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);
            RegisterModules(services);

            return services.BuildServiceProvider();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            //services.AddLogging(configure =>
            //{
            //    configure.AddConsole(o => o.FormatterName = nameof(CustomConsoleFormatter))
            //             .AddConsoleFormatter<CustomConsoleFormatter, CustomConsoleOptions>();
            //    configure.AddDebug();
            //});
        }

        private static void RegisterModules(IServiceCollection services)
        {
            services.RegisterModule<ApplicationModule>();
        }
    }
}