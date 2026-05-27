using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Floating.Windows.Abstract;
using Soenneker.Blazor.Interops.Floating.Registrars;

namespace Soenneker.Blazor.Floating.Windows.Registrars;

/// <summary>
/// A Blazor UI element for a drag and drop, overlayed window
/// </summary>
public static class FloatingWindowRegistrar
{
    /// <summary>
    /// Adds <see cref="IFloatingWindow"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddFloatingWindowAsScoped(this IServiceCollection services)
    {
        services.AddFloatingUiInteropAsScoped().TryAddScoped<IFloatingWindowInterop, FloatingWindowInterop>();

        return services;
    }
}