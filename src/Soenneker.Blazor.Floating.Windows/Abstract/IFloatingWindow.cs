using Microsoft.AspNetCore.Components;
using Soenneker.Blazor.Floating.Windows.Dtos;
using Soenneker.Blazor.Floating.Windows.Options;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Lepton.Suite.Abstract;

namespace Soenneker.Blazor.Floating.Windows.Abstract;

/// <summary>
/// Represents a floating window component instance with customizable appearance, behavior, and lifecycle methods.
/// </summary>
public interface IFloatingWindow : ILeptonCancellableIdentifiableContentElement
{
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
    /// Override: Whether the window is enabled and active.
    /// </summary>
    bool? Enabled { get; set; }

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
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the show operation is complete.</returns>
    ValueTask Show(CancellationToken cancellationToken = default);

    /// <summary>
    /// Hides the window.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the hide operation is complete.</returns>
    ValueTask Hide(CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggles the window visibility.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the toggle operation is complete.</returns>
    ValueTask Toggle(CancellationToken cancellationToken = default);

    /// <summary>
    /// Closes the window (hides and disposes).
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the close operation is complete.</returns>
    ValueTask Close(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current position of the window.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested (int x, int y).</returns>
    ValueTask<(int x, int y)> GetPosition(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the position of the window.
    /// </summary>
    /// <param name="x">Operand passed to the accumulator function.</param>
    /// <param name="y">Vertical coordinate to apply.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the position has been stored.</returns>
    ValueTask SetPosition(int x, int y, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current size of the window.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A WindowSize object containing the width and height.</returns>
    ValueTask<FloatingWindowSize> GetSize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the size of the window.
    /// </summary>
    /// <param name="width">Width to apply.</param>
    /// <param name="height">Height to apply.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the size has been stored.</returns>
    ValueTask SetSize(int width, int height, CancellationToken cancellationToken = default);

    /// <summary>
    /// Centers the window in the viewport, accounting for its width and height.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the center operation is complete.</returns>
    ValueTask Center(CancellationToken cancellationToken = default);
}
