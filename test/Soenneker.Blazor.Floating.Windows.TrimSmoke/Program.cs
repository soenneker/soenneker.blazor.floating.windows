using System.Text.Json;
using Soenneker.Blazor.Floating.Windows;

var configuration = JsonSerializer.Deserialize("{}", LibraryJsonContext.Default.FloatingWindowOptions)!;
var payload = JsonSerializer.SerializeToElement(configuration, LibraryJsonContext.Default.FloatingWindowOptions);
Check(payload.ValueKind == JsonValueKind.Object, "configuration wire object");
var options = LibraryJsonContext.WithContext(SmokeJsonContext.Default);
var custom = JsonSerializer.SerializeToElement<object>(new SmokePayload { Value = "custom" }, (System.Text.Json.Serialization.Metadata.JsonTypeInfo<object>)options.GetTypeInfo(typeof(object)));
Check(custom.GetProperty("value").GetString() == "custom", "application-generated metadata");
var position = JsonSerializer.Deserialize("""{"x":125,"y":-40}""", LibraryJsonContext.Default.FloatingWindowPosition)!;
Check(position.X == 125 && position.Y == -40, "named position coordinates");

Console.WriteLine("Trimmed JSON smoke checks passed.");

static void Check(bool condition, string name)
{
    if (!condition) throw new InvalidOperationException(name);
}
