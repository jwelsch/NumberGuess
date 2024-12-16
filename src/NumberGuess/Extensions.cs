using Avalonia.Controls;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Styling;

namespace NumberGuess
{
    internal static class Extensions
    {
        public static T? FindResourceExt<T>(this IResourceHost host, ThemeVariant? theme, object key)
        {
            var resource = host.FindResource(theme, key);

            if (resource is not null
                && resource is DynamicResourceExtension dynamicResourceExtension
                && dynamicResourceExtension.ResourceKey != null)
            {
                return host.FindResourceExt<T>(theme, dynamicResourceExtension.ResourceKey);
            }
            else if (resource is T output)
            {
                return output;
            }

            return default;
        }
    }
}
