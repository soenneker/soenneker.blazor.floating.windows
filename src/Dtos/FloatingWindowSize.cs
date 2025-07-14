using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Floating.Windows.Dtos;

public sealed class FloatingWindowSize
{
    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}