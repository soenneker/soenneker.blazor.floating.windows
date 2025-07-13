using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Blazor.Floating.Windows.Options;

namespace Soenneker.Blazor.Floating.Windows.Abstract;

/// <summary>
/// Represents a floating Window component instance with customizable appearance, behavior, and lifecycle methods.
/// </summary>
public interface IFloatingWindow : IAsyncDisposable
{
    /// <summary>
    /// The Window content as a plain string. Mutually exclusive with <see cref="SetWindowContent"/>.
    /// </summary>
    string? Text { get; set; }

    /// <summary>
    /// The unique identifier used internally for Window registration and DOM references.
    /// </summary>
    string Id { get; set; }

    /// <summary>
    /// Optional parameters applied to the Window container.
    /// </summary>
    Dictionary<string, object?>? WindowAttributes { get; set; }

    /// <summary>
    /// Optional parameters applied to the Window anchor element.
    /// </summary>
    Dictionary<string, object?>? AnchorAttributes { get; set; }

    /// <summary>
    /// The main child content that serves as the anchor target for the Window.
    /// </summary>
    RenderFragment ChildContent { get; set; }

    /// <summary>
    /// Callback triggered when the Window becomes visible.
    /// </summary>
    EventCallback OnShow { get; set; }

    /// <summary>
    /// Callback triggered when the Window becomes hidden.
    /// </summary>
    EventCallback OnHide { get; set; }

    /// <summary>
    /// The full set of Window configuration options. Individual properties take precedence over this object.
    /// </summary>
    FloatingWindowOptions Options { get; set; }

    /// <summary>
    /// Override: Whether the Window animates on show/hide.
    /// </summary>
    bool? Animate { get; set; }

    /// <summary>
    /// Override: Delay in milliseconds before the Window is shown.
    /// </summary>
    int? ShowDelay { get; set; }

    /// <summary>
    /// Override: Delay in milliseconds before the Window is hidden.
    /// </summary>
    int? HideDelay { get; set; }

    /// <summary>
    /// Override: Whether to show the Window arrow.
    /// </summary>
    bool? ShowArrow { get; set; }

    /// <summary>
    /// Override: Whether the Window is interactive (can receive pointer events).
    /// </summary>
    bool? Interactive { get; set; }

    /// <summary>
    /// Override: Whether the Window is enabled and active.
    /// </summary>
    bool? Enabled { get; set; }

    /// <summary>
    /// Override: Maximum width of the Window in pixels.
    /// </summary>
    int? MaxWidth { get; set; }

    /// <summary>
    /// Override: If true, the Window must be manually triggered to show/hide.
    /// </summary>
    bool? ManualTrigger { get; set; }

    /// <summary>
    /// Override: Whether resources like scripts and styles should be loaded from CDN.
    /// </summary>
    bool? UseCdn { get; set; }

    /// <summary>
    /// Provides Window content as a RenderFragment (instead of plain text).
    /// </summary>
    /// <param name="content">The content to be rendered inside the Window.</param>
    void SetWindowContent(RenderFragment content);

    /// <summary>
    /// Shows the Window manually. Only applicable if <see cref="ManualTrigger"/> is <c>true</c>.
    /// </summary>
    ValueTask Show();

    /// <summary>
    /// Hides the Window manually. Only applicable if <see cref="ManualTrigger"/> is <c>true</c>.
    /// </summary>
    ValueTask Hide();

    /// <summary>
    /// Toggles the Window visibility manually. Only applicable if <see cref="ManualTrigger"/> is <c>true</c>.
    /// </summary>
    ValueTask Toggle();
}