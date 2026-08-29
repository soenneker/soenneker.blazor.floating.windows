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
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddFloatingWindowAsScoped(this IServiceCollection services)
    {
        services.AddFloatingUiInteropAsScoped().TryAddScoped<IFloatingWindowInterop, FloatingWindowInterop>();

        return services;
    }
}
