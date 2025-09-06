using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Floating.Windows.Options;

/// <summary>
/// Options for configuring a floating window.
/// </summary>
public sealed class FloatingWindowOptions
{
    /// <summary>
    /// Whether the window is draggable by its title bar.
    /// </summary>
    [JsonPropertyName("draggable")]
    public bool Draggable { get; set; } = true;

    /// <summary>
    /// Whether the window is resizable by dragging its edges.
    /// </summary>
    [JsonPropertyName("resizable")]
    public bool Resizable { get; set; } = true;

    /// <summary>
    /// Whether the window has a close button in the title bar.
    /// </summary>
    [JsonPropertyName("showCloseButton")]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// Whether the window has a title bar.
    /// </summary>
    [JsonPropertyName("showTitleBar")]
    public bool ShowTitleBar { get; set; } = true;

    /// <summary>
    /// The title text for the window.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Whether this window is enabled. If false, nothing is shown.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Initial width of the window in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    public int? Width { get; set; } = 400;

    /// <summary>
    /// Initial height of the window in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    public int? Height { get; set; } = 300;

    /// <summary>
    /// Initial X position of the window.
    /// </summary>
    [JsonPropertyName("initialX")]
    public int? InitialX { get; set; } = 100;

    /// <summary>
    /// Initial Y position of the window.
    /// </summary>
    [JsonPropertyName("initialY")]
    public int? InitialY { get; set; } = 100;

    /// <summary>
    /// Minimum width the window can be resized to.
    /// </summary>
    [JsonPropertyName("minWidth")]
    public int MinWidth { get; set; } = 200;

    /// <summary>
    /// Minimum height the window can be resized to.
    /// </summary>
    [JsonPropertyName("minHeight")]
    public int MinHeight { get; set; } = 150;

    /// <summary>
    /// Maximum width the window can be resized to.
    /// </summary>
    [JsonPropertyName("maxWidth")]
    public int? MaxWidth { get; set; }

    /// <summary>
    /// Maximum height the window can be resized to.
    /// </summary>
    [JsonPropertyName("maxHeight")]
    public int? MaxHeight { get; set; }

    /// <summary>
    /// Whether the window should be constrained to the viewport bounds.
    /// </summary>
    [JsonPropertyName("constrainToViewport")]
    public bool ConstrainToViewport { get; set; } = true;

    /// <summary>
    /// Whether the window should be centered on screen when first shown. Defaults to true.
    /// </summary>
    [JsonPropertyName("centerOnShow")]
    public bool CenterOnShow { get; set; } = true;

    /// <summary>
    /// Whether the window should be focused when shown.
    /// </summary>
    [JsonPropertyName("focusOnShow")]
    public bool FocusOnShow { get; set; } = true;

    /// <summary>
    /// Whether resources like scripts and styles are loaded from CDN.
    /// </summary>
    [JsonPropertyName("useCdn")]
    public bool UseCdn { get; set; } = true;

    /// <summary>
    /// Whether the window should automatically resize to fit its content (overrides width/height if true). Defaults to true.
    /// </summary>
    [JsonPropertyName("autoSizeToContent")]
    public bool AutoSizeToContent { get; set; } = true;

    /// <summary>
    /// Whether the window should dynamically resize to fit its content as it changes (uses ResizeObserver).
    /// </summary>
    [JsonPropertyName("dynamicAutoSizeToContent")]
    public bool DynamicAutoSizeToContent { get; set; }

    /// <summary>
    /// Whether the window should automatically re-center itself when its content size changes (requires auto size to content or dynamic auto size to content). Defaults to false.
    /// </summary>
    [JsonPropertyName("recenterOnResize")]
    public bool RecenterOnResize { get; set; }

    /// <summary>
    /// Z-index for the window. Higher values appear on top.
    /// </summary>
    [JsonPropertyName("zIndex")]
    public int ZIndex { get; set; } = 1000;

    /// <summary>
    /// Style options for configuring the appearance of the window.
    /// </summary>
    [JsonPropertyName("styleOptions")]
    public FloatingWindowStyleOptions StyleOptions { get; set; } = FloatingWindowStyleOptions.DefaultLight;
}
