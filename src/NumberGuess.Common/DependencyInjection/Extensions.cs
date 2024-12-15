using Microsoft.Extensions.DependencyInjection;

namespace NumberGuess.Common.DependencyInjection
{
    public static class Extensions
    {
        public static void RegisterModule<TModule>(this IServiceCollection services, object[]? parameters = null) where TModule : Module
        {
            var module = Activator.CreateInstance<TModule>();
            module.LoadModule(services, parameters);
        }
    }
}