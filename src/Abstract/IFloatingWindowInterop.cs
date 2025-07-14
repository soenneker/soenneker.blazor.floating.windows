using Microsoft.JSInterop;
using Soenneker.Blazor.Floating.Windows.Dtos;
using Soenneker.Blazor.Floating.Windows.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Floating.Windows.Abstract;

/// <summary>
/// Provides JavaScript interop methods for initializing, controlling, and destroying Floating UI-based windows in a Blazor application.
/// </summary>
public interface IFloatingWindowInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures all required JavaScript and CSS resources for Floating UI windows are loaded.
    /// This includes loading from CDN or embedded static files, depending on <paramref name="useCdn"/>.
    /// </summary>
    /// <param name="useCdn">Whether to load scripts from CDN or from local embedded resources.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new window instance using Floating UI logic and registers it for dragging and resizing.
    /// </summary>
    /// <param name="id">The unique ID used to resolve window DOM elements.</param>
    /// <param name="options">The configuration options used for window behavior.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Create(string id, FloatingWindowOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers a Blazor component's instance for receiving window lifecycle event callbacks.
    /// </summary>
    /// <param name="id">The unique window ID.</param>
    /// <param name="dotNetRef">A .NET reference to the component implementing the JSInvokable event handlers.</param>
    ValueTask SetCallbacks(string id, DotNetObjectReference<FloatingWindow> dotNetRef);

    /// <summary>
    /// Destroys a previously initialized window instance, unregistering its events and observers.
    /// </summary>
    /// <param name="id">The ID of the window to clean up.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Destroy(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Shows the window.
    /// </summary>
    /// <param name="id">The window ID.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Show(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Hides the window.
    /// </summary>
    /// <param name="id">The window ID.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Hide(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggles the window visibility.
    /// </summary>
    /// <param name="id">The window ID.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Toggle(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Closes the window (hides and disposes).
    /// </summary>
    /// <param name="id">The window ID.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Close(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current position of the window.
    /// </summary>
    /// <param name="id">The window ID.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A tuple containing the x and y coordinates.</returns>
    ValueTask<(int x, int y)> GetPosition(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the position of the window.
    /// </summary>
    /// <param name="id">The window ID.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask SetPosition(string id, int x, int y, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current size of the window.
    /// </summary>
    /// <param name="id">The window ID.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A WindowSize object containing the width and height.</returns>
    ValueTask<FloatingWindowSize> GetSize(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the size of the window.
    /// </summary>
    /// <param name="id">The window ID.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask SetSize(string id, int width, int height, CancellationToken cancellationToken = default);

    /// <summary>
    /// Brings the window to the front (highest z-index).
    /// </summary>
    /// <param name="id">The window ID.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask BringToFront(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the viewport dimensions.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A FloatingWindowVieportSize object containing the viewport width and height.</returns>
    ValueTask<FloatingWindowSize> GetViewportSize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Centers the window in the viewport, accounting for its current size, without making it visible.
    /// </summary>
    /// <param name="id">The window ID.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask CenterInViewport(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disposes internal state and JavaScript module references.
    /// </summary>
    new ValueTask DisposeAsync();
}
