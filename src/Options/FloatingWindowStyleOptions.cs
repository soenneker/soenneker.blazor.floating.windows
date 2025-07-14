using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Floating.Windows.Options;

/// <summary>
/// Style options for configuring the appearance of a floating window.
/// </summary>
public class FloatingWindowStyleOptions
{
    /// <summary>
    /// Background color of the window body.
    /// </summary>
    [JsonPropertyName("backgroundColor")]
    public string BackgroundColor { get; set; } = "#ffffff";

    /// <summary>
    /// Border color of the window.
    /// </summary>
    [JsonPropertyName("borderColor")]
    public string BorderColor { get; set; } = "#d1d5db";

    /// <summary>
    /// Border width of the window.
    /// </summary>
    [JsonPropertyName("borderWidth")]
    public string BorderWidth { get; set; } = "1px";

    /// <summary>
    /// Border radius of the window.
    /// </summary>
    [JsonPropertyName("borderRadius")]
    public string BorderRadius { get; set; } = "8px";

    /// <summary>
    /// Box shadow of the window.
    /// </summary>
    [JsonPropertyName("boxShadow")]
    public string BoxShadow { get; set; } = "0 10px 25px rgba(0, 0, 0, 0.15), 0 4px 10px rgba(0, 0, 0, 0.1)";

    /// <summary>
    /// Text color of the window content.
    /// </summary>
    [JsonPropertyName("textColor")]
    public string TextColor { get; set; } = "#374151";

    /// <summary>
    /// Font family for the window.
    /// </summary>
    [JsonPropertyName("fontFamily")]
    public string FontFamily { get; set; } = "-apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif";

    /// <summary>
    /// Font size for the window content.
    /// </summary>
    [JsonPropertyName("fontSize")]
    public string FontSize { get; set; } = "14px";

    /// <summary>
    /// Line height for the window content.
    /// </summary>
    [JsonPropertyName("lineHeight")]
    public string LineHeight { get; set; } = "1.5";

    /// <summary>
    /// Title bar background color.
    /// </summary>
    [JsonPropertyName("titleBarBackgroundColor")]
    public string TitleBarBackgroundColor { get; set; } = "#f8f9fa";

    /// <summary>
    /// Title bar background gradient (overrides backgroundColor if set).
    /// </summary>
    [JsonPropertyName("titleBarBackgroundGradient")]
    public string? TitleBarBackgroundGradient { get; set; }

    /// <summary>
    /// Title bar text color.
    /// </summary>
    [JsonPropertyName("titleBarTextColor")]
    public string TitleBarTextColor { get; set; } = "#374151";

    /// <summary>
    /// Title bar font weight.
    /// </summary>
    [JsonPropertyName("titleBarFontWeight")]
    public string TitleBarFontWeight { get; set; } = "600";

    /// <summary>
    /// Title bar font size.
    /// </summary>
    [JsonPropertyName("titleBarFontSize")]
    public string TitleBarFontSize { get; set; } = "14px";

    /// <summary>
    /// Title bar padding.
    /// </summary>
    [JsonPropertyName("titleBarPadding")]
    public string TitleBarPadding { get; set; } = "12px 16px";

    /// <summary>
    /// Title bar border bottom color.
    /// </summary>
    [JsonPropertyName("titleBarBorderBottomColor")]
    public string TitleBarBorderBottomColor { get; set; } = "#e9ecef";

    /// <summary>
    /// Close button background color.
    /// </summary>
    [JsonPropertyName("closeButtonBackgroundColor")]
    public string CloseButtonBackgroundColor { get; set; } = "transparent";

    /// <summary>
    /// Close button text color.
    /// </summary>
    [JsonPropertyName("closeButtonTextColor")]
    public string CloseButtonTextColor { get; set; } = "#6b7280";

    /// <summary>
    /// Close button hover background color.
    /// </summary>
    [JsonPropertyName("closeButtonHoverBackgroundColor")]
    public string CloseButtonHoverBackgroundColor { get; set; } = "#f3f4f6";

    /// <summary>
    /// Close button active background color.
    /// </summary>
    [JsonPropertyName("closeButtonActiveBackgroundColor")]
    public string CloseButtonActiveBackgroundColor { get; set; } = "#e5e7eb";

    /// <summary>
    /// Close button border radius.
    /// </summary>
    [JsonPropertyName("closeButtonBorderRadius")]
    public string CloseButtonBorderRadius { get; set; } = "4px";

    /// <summary>
    /// Close button font size.
    /// </summary>
    [JsonPropertyName("closeButtonFontSize")]
    public string CloseButtonFontSize { get; set; } = "18px";

    /// <summary>
    /// Content area padding.
    /// </summary>
    [JsonPropertyName("contentPadding")]
    public string ContentPadding { get; set; } = "16px";

    /// <summary>
    /// Resize handle hover background color.
    /// </summary>
    [JsonPropertyName("resizeHandleHoverBackgroundColor")]
    public string ResizeHandleHoverBackgroundColor { get; set; } = "rgba(0, 0, 0, 0.05)";

    /// <summary>
    /// Focus border color.
    /// </summary>
    [JsonPropertyName("focusBorderColor")]
    public string FocusBorderColor { get; set; } = "rgba(59, 130, 246, 0.3)";

    /// <summary>
    /// Focus border width.
    /// </summary>
    [JsonPropertyName("focusBorderWidth")]
    public string FocusBorderWidth { get; set; } = "2px";

    /// <summary>
    /// Transition duration for animations.
    /// </summary>
    [JsonPropertyName("transitionDuration")]
    public string TransitionDuration { get; set; } = "0.2s";

    /// <summary>
    /// Transition timing function for animations.
    /// </summary>
    [JsonPropertyName("transitionTimingFunction")]
    public string TransitionTimingFunction { get; set; } = "ease";

    /// <summary>
    /// Minimum width of the window.
    /// </summary>
    [JsonPropertyName("minWidth")]
    public string MinWidth { get; set; } = "200px";

    /// <summary>
    /// Minimum height of the window.
    /// </summary>
    [JsonPropertyName("minHeight")]
    public string MinHeight { get; set; } = "150px";

    /// <summary>
    /// Z-index of the window.
    /// </summary>
    [JsonPropertyName("zIndex")]
    public int ZIndex { get; set; } = 1000;

    /// <summary>
    /// Whether to use dark mode styling.
    /// </summary>
    [JsonPropertyName("useDarkMode")]
    public bool UseDarkMode { get; set; } = false;

    /// <summary>
    /// Custom CSS class to apply to the window.
    /// </summary>
    [JsonPropertyName("customClass")]
    public string? CustomClass { get; set; }

    /// <summary>
    /// Custom CSS styles to apply to the window.
    /// </summary>
    [JsonPropertyName("customStyles")]
    public Dictionary<string, string>? CustomStyles { get; set; }

    /// <summary>
    /// Creates a default light theme style configuration.
    /// </summary>
    public static FloatingWindowStyleOptions DefaultLight => new();

    /// <summary>
    /// Creates a default dark theme style configuration.
    /// </summary>
    public static FloatingWindowStyleOptions DefaultDark => new()
    {
        BackgroundColor = "#1f2937",
        BorderColor = "#374151",
        TextColor = "#f9fafb",
        TitleBarBackgroundColor = "#111827",
        TitleBarTextColor = "#f9fafb",
        TitleBarBorderBottomColor = "#374151",
        CloseButtonTextColor = "#9ca3af",
        CloseButtonHoverBackgroundColor = "#374151",
        CloseButtonActiveBackgroundColor = "#4b5563",
        ResizeHandleHoverBackgroundColor = "rgba(255, 255, 255, 0.1)",
        UseDarkMode = true
    };
} 