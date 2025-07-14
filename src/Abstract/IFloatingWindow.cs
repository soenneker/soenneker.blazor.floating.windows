using Microsoft.AspNetCore.Components;
using Soenneker.Blazor.Floating.Windows.Dtos;
using Soenneker.Blazor.Floating.Windows.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Floating.Windows.Abstract;

/// <summary>
/// Represents a floating window component instance with customizable appearance, behavior, and lifecycle methods.
/// </summary>
public interface IFloatingWindow : IAsyncDisposable
{
    /// <summary>
    /// The unique identifier used internally for window registration and DOM references.
    /// </summary>
    string ElementId { get; set; }

    /// <summary>
    /// Optional parameters applied to the window container.
    /// </summary>
    Dictionary<string, object?>? WindowAttributes { get; set; }

    /// <summary>
    /// The main child content that will be rendered inside the floating window.
    /// </summary>
    RenderFragment ChildContent { get; set; }

    /// <summary>
    /// Callback triggered when the window becomes visible.
    /// </summary>
    EventCallback OnShow { get; set; }

    /// <summary>
    /// Callback triggered when the window becomes hidden.
    /// </summary>
    EventCallback OnHide { get; set; }

    /// <summary>
    /// Callback triggered when the window starts dragging.
    /// </summary>
    EventCallback OnDragStart { get; set; }

    /// <summary>
    /// Callback triggered when the window stops dragging.
    /// </summary>
    EventCallback OnDragEnd { get; set; }

    /// <summary>
    /// The full set of window configuration options. Individual properties take precedence over this object.
    /// </summary>
    FloatingWindowOptions Options { get; set; }

    /// <summary>
    /// Override: Whether the window is draggable.
    /// </summary>
    bool? Draggable { get; set; }

    /// <summary>
    /// Override: Whether the window is resizable.
    /// </summary>
    bool? Resizable { get; set; }

    /// <summary>
    /// Override: Whether the window has a close button.
    /// </summary>
    bool? ShowCloseButton { get; set; }

    /// <summary>
    /// Override: Whether the window has a title bar.
    /// </summary>
    bool? ShowTitleBar { get; set; }

    /// <summary>
    /// Override: The title text for the window.
    /// </summary>
    string? Title { get; set; }

    /// <summary>
    /// Override: Whether the window is enabled and active.
    /// </summary>
    bool? Enabled { get; set; }

    /// <summary>
    /// Override: Initial width of the window in pixels.
    /// </summary>
    int? Width { get; set; }

    /// <summary>
    /// Override: Initial height of the window in pixels.
    /// </summary>
    int? Height { get; set; }

    /// <summary>
    /// Override: Initial X position of the window.
    /// </summary>
    int? InitialX { get; set; }

    /// <summary>
    /// Override: Initial Y position of the window.
    /// </summary>
    int? InitialY { get; set; }

    /// <summary>
    /// Override: Whether resources like scripts and styles should be loaded from CDN.
    /// </summary>
    bool? UseCdn { get; set; }

    /// <summary>
    /// Override: Whether the window should automatically resize to fit its content (overrides width/height if true).
    /// </summary>
    bool? AutoSizeToContent { get; set; }

    /// <summary>
    /// Override: Whether the window should dynamically resize to fit its content as it changes.
    /// </summary>
    bool? DynamicAutoSizeToContent { get; set; }

    /// <summary>
    /// Shows the window.
    /// </summary>
    ValueTask Show(CancellationToken cancellationToken = default);

    /// <summary>
    /// Hides the window.
    /// </summary>
    ValueTask Hide(CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggles the window visibility.
    /// </summary>
    ValueTask Toggle(CancellationToken cancellationToken = default);

    /// <summary>
    /// Closes the window (hides and disposes).
    /// </summary>
    ValueTask Close(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current position of the window.
    /// </summary>
    ValueTask<(int x, int y)> GetPosition(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the position of the window.
    /// </summary>
    ValueTask SetPosition(int x, int y, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current size of the window.
    /// </summary>
    /// <returns>A WindowSize object containing the width and height.</returns>
    ValueTask<FloatingWindowSize> GetSize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the size of the window.
    /// </summary>
    ValueTask SetSize(int width, int height, CancellationToken cancellationToken = default);

    /// <summary>
    /// Centers the window in the viewport, accounting for its width and height.
    /// </summary>
    ValueTask Center(CancellationToken cancellationToken = default);
}