using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Floating.Windows.Options;

/// <summary>
/// Options for configuring a floating Window.
/// </summary>
public class FloatingWindowOptions
{
    /// <summary>
    /// Whether the Window should animate on show/hide.
    /// </summary>
    [JsonPropertyName("animate")]
    public bool Animate { get; set; } = true;

    /// <summary>
    /// Delay in milliseconds before showing the Window (if not manually triggered).
    /// </summary>
    [JsonPropertyName("showDelay")]
    public int ShowDelay { get; set; }

    /// <summary>
    /// Delay in milliseconds before hiding the Window (if not manually triggered).
    /// </summary>
    [JsonPropertyName("hideDelay")]
    public int HideDelay { get; set; }

    /// <summary>
    /// Whether to render the arrow pointing to the reference element.
    /// </summary>
    [JsonPropertyName("showArrow")]
    public bool ShowArrow { get; set; } = true;

    /// <summary>
    /// Whether the Window can receive pointer events (e.g. buttons/links inside).
    /// </summary>
    [JsonPropertyName("interactive")]
    public bool Interactive { get; set; } = false;

    /// <summary>
    /// Whether this Window is enabled. If false, nothing is shown.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Optional maximum width (in pixels) of the Window.
    /// </summary>
    [JsonPropertyName("maxWidth")]
    public int? MaxWidth { get; set; }

    /// <summary>
    /// If true, Window will only show or hide when triggered manually via C#.
    /// </summary>
    [JsonPropertyName("manualTrigger")]
    public bool ManualTrigger { get; set; } = false;

    /// <summary>
    /// If true, resources like scripts and styles are loaded from CDN.
    /// </summary>
    [JsonPropertyName("useCdn")]
    public bool UseCdn { get; set; } = true;
}