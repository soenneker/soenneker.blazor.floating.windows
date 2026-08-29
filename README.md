[![](https://img.shields.io/nuget/v/soenneker.blazor.floating.windows.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.floating.windows/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.floating.windows/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.floating.windows/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.floating.windows.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.floating.windows/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.blazor.floating.windows/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.floating.windows/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.floating.windows/actions/workflows/codeql.yml)

# Soenneker.Blazor.Floating.Windows

A Blazor component for mouse-draggable, resizable floating panels with programmatic visibility, positioning, sizing, and typed styling.

## Installation

```bash
dotnet add package Soenneker.Blazor.Floating.Windows
```

```csharp
using Soenneker.Blazor.Floating.Windows.Registrars;

builder.Services.AddFloatingWindowAsScoped();
```

Add the namespaces to `_Imports.razor`:

```razor
@using Soenneker.Blazor.Floating.Windows
@using Soenneker.Blazor.Floating.Windows.Options
```

## Usage

Windows start hidden. Keep a component reference and call `Show()` from a user action after the component has rendered.

```razor
<button type="button" @onclick="Open">Open inspector</button>

<FloatingWindow @ref="_window"
                Title="Order inspector"
                Options="_options"
                OnDragEnd="SavePosition">
    <p>Order details go here.</p>
    <button type="button" @onclick="Close">Done</button>
</FloatingWindow>

@code {
    private FloatingWindow? _window;

    private readonly FloatingWindowOptions _options = new()
    {
        Width = 480,
        Height = 320,
        AutoSizeToContent = false,
        MinWidth = 280,
        MinHeight = 180,
        CenterOnShow = true,
        ConstrainToViewport = true,
        StyleOptions = FloatingWindowStyleOptions.DefaultDark
    };

    private Task Open() => _window!.Show().AsTask();
    private Task Close() => _window!.Hide().AsTask();

    private async Task SavePosition()
    {
        (int x, int y) = await _window!.GetPosition();
        // Persist x and y if desired.
    }
}
```

Set `Width` and `Height` through `FloatingWindowOptions`. When `AutoSizeToContent` is `true` (the default), explicit width and height are ignored. `DynamicAutoSizeToContent` observes later content-size changes; combine it with `RecenterOnResize` when the panel should remain centered.

## Control methods

```csharp
await _window!.Show();
await _window.Hide();
await _window.Toggle();
await _window.SetPosition(120, 80);
await _window.SetSize(640, 480);

(int x, int y) = await _window.GetPosition();
FloatingWindowSize size = await _window.GetSize();
```

`Close()` currently has the same visibility behavior as `Hide()`; it does not remove the Razor component. `Destroy()` releases the JavaScript behavior and is normally handled by component disposal.

## Header and styling

Use `HeaderContent` to replace the title text while retaining the title bar and close button:

```razor
<FloatingWindow @ref="_window" StyleOptions="_style">
    <HeaderContent>
        <strong>Live metrics</strong>
    </HeaderContent>
    <p>Content</p>
</FloatingWindow>

@code {
    private FloatingWindow? _window;

    private readonly FloatingWindowStyleOptions _style = new()
    {
        BackgroundColor = "#111827",
        TextColor = "#f9fafb",
        TitleBarBackgroundColor = "#1f2937",
        TitleBarTextColor = "#f9fafb",
        BorderRadius = "10px"
    };
}
```

`DefaultLight` and `DefaultDark` return new style instances that can be customized safely. `CustomStyles` is emitted directly into the element's inline `style`; only populate it from trusted application configuration.

## Behavior notes

- Dragging uses the title bar; hiding the title bar removes the drag handle.
- Resizing uses the window edges and corners. The current implementation uses mouse events, so do not rely on drag/resize as the only path on touch devices.
- Multiple windows move forward in z-order when dragged or resized. Set the initial `ZIndex` in `FloatingWindowOptions`.
- `OnShow`, `OnHide`, `OnDragStart`, and `OnDragEnd` run after the corresponding browser-side state change.
- Options are applied when the component is initialized. Use `@key` to recreate it when configuration must change at runtime.
