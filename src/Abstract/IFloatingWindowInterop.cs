using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Blazor.Floating.Windows.Options;

namespace Soenneker.Blazor.Floating.Windows.Abstract;

/// <summary>
/// Provides JavaScript interop methods for initializing, controlling, and destroying Floating UI-based Windows in a Blazor application.
/// </summary>
public interface IFloatingWindowInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures all required JavaScript and CSS resources for Floating UI Windows are loaded.
    /// This includes loading from CDN or embedded static files, depending on <paramref name="useCdn"/>.
    /// </summary>
    /// <param name="useCdn">Whether to load scripts from CDN or from local embedded resources.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new Window instance using Floating UI logic, attaches it to a DOM anchor element, and registers it for automatic positioning.
    /// </summary>
    /// <param name="id">The unique ID used to resolve anchor and Window DOM elements (e.g., anchor-{id}, Window-{id}).</param>
    /// <param name="options">The configuration options used for positioning and middleware setup.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Create(string id, FloatingWindowOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers a Blazor component's instance for receiving Window lifecycle event callbacks (e.g., OnShow, OnHide).
    /// </summary>
    /// <param name="id">The unique Window ID corresponding to the anchor element.</param>
    /// <param name="dotNetRef">A .NET reference to the component implementing the JSInvokable event handlers.</param>
    ValueTask SetCallbacks(string id, DotNetObjectReference<FloatingWindow> dotNetRef);

    /// <summary>
    /// Destroys a previously initialized Window instance, unregistering its events and observers.
    /// </summary>
    /// <param name="id">The ID of the Window/anchor pair to clean up.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Destroy(string id, CancellationToken cancellationToken = default);

    ValueTask Show(string id, CancellationToken cancellationToken = default);

    ValueTask Hide(string id, CancellationToken cancellationToken = default);

    ValueTask Toggle(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disposes internal state and JavaScript module references.
    /// </summary>
    new ValueTask DisposeAsync();
}
