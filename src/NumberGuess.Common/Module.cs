using Microsoft.Extensions.DependencyInjection;

namespace NumberGuess.Common
{
    public abstract class Module
    {
        public void LoadModule(IServiceCollection services, object[]? parameters = null)
        {
            Load(services, parameters);
        }

        protected virtual void Load(IServiceCollection services, object[]? parameters = null)
        {
        }
    }
}
