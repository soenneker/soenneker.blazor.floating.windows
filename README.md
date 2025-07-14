[![](https://img.shields.io/nuget/v/soenneker.blazor.floating.windows.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.floating.windows/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.floating.windows/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.floating.windows/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.floating.windows.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.floating.windows/)

# Soenneker.Blazor.Floating.Windows

A Blazor component library for creating draggable, resizable floating windows with modern styling and full customization options.

## Features

- ✅ **Draggable Windows** - Drag windows by their title bar
- ✅ **Resizable Windows** - Resize by dragging edges and corners
- ✅ **Modern Styling** - Beautiful gradient title bars and smooth animations
- ✅ **Customizable** - Full control over appearance and behavior
- ✅ **Viewport Constraints** - Keep windows within screen bounds
- ✅ **Z-Index Management** - Automatic focus and layering
- ✅ **Responsive Design** - Works on desktop and mobile
- ✅ **Dark Mode Support** - Automatic theme detection
- ✅ **Accessibility** - Proper ARIA labels and keyboard support
- ✅ **Event Callbacks** - React to show, hide, drag, and resize events

## Installation

1. Add the NuGet package to your project:
```bash
dotnet add package Soenneker.Blazor.Floating.Windows
```

2. Register the services in your `Program.cs`:
```csharp
using Soenneker.Blazor.Floating.Windows.Registrars;

builder.Services.AddFloatingWindowAsScoped();
```

3. Add the using statement to your `_Imports.razor`:
```csharp
@using Soenneker.Blazor.Floating.Windows
@using Soenneker.Blazor.Floating.Windows.Options
```

## Basic Usage

```razor
<FloatingWindow Title="My Window" Width="400" Height="300">
    <div>
        <h3>Hello World!</h3>
        <p>This is a floating window with your content.</p>
        <button @onclick="() => window?.Close()">Close</button>
    </div>
</FloatingWindow>
```

## Advanced Usage

```razor
<FloatingWindow @ref="myWindow"
                Title="Advanced Window"
                Width="500"
                Height="400"
                InitialX="100"
                InitialY="100"
                Draggable="true"
                Resizable="true"
                ShowCloseButton="true"
                ShowTitleBar="true"
                CenterOnShow="true"
                ConstrainToViewport="true"
                OnShow="OnWindowShow"
                OnHide="OnWindowHide"
                OnDragStart="OnDragStart"
                OnDragEnd="OnDragEnd">
    <div class="window-content">
        <h3>Advanced Features</h3>
        <p>This window demonstrates all the available features.</p>
        <div class="controls">
            <button @onclick="() => myWindow?.SetPosition(200, 200)">Move to (200,200)</button>
            <button @onclick="() => myWindow?.SetSize(600, 500)">Resize to 600x500</button>
            <button @onclick="() => myWindow?.BringToFront()">Bring to Front</button>
        </div>
    </div>
</FloatingWindow>

@code {
    private FloatingWindow? myWindow;

    private void OnWindowShow()
    {
        Console.WriteLine("Window shown!");
    }

    private void OnWindowHide()
    {
        Console.WriteLine("Window hidden!");
    }

    private void OnDragStart()
    {
        Console.WriteLine("Dragging started!");
    }

    private void OnDragEnd()
    {
        Console.WriteLine("Dragging ended!");
    }
}
```

## Configuration Options

### Window Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Title` | `string` | `"Window"` | The title displayed in the title bar |
| `Width` | `int?` | `400` | Initial width in pixels |
| `Height` | `int?` | `300` | Initial height in pixels |
| `InitialX` | `int?` | `100` | Initial X position |
| `InitialY` | `int?` | `100` | Initial Y position |
| `Draggable` | `bool?` | `true` | Whether the window can be dragged |
| `Resizable` | `bool?` | `true` | Whether the window can be resized |
| `ShowCloseButton` | `bool?` | `true` | Whether to show the close button |
| `ShowTitleBar` | `bool?` | `true` | Whether to show the title bar |

### Event Callbacks

| Event | Description |
|-------|-------------|
| `OnShow` | Triggered when the window becomes visible |
| `OnHide` | Triggered when the window becomes hidden |
| `OnDragStart` | Triggered when dragging begins |
| `OnDragEnd` | Triggered when dragging ends |

### Methods

| Method | Description |
|--------|-------------|
| `Show()` | Shows the window |
| `Hide()` | Hides the window |
| `Toggle()` | Toggles window visibility |
| `Close()` | Hides and disposes the window |
| `GetPosition()` | Returns current (x, y) position |
| `SetPosition(x, y)` | Sets the window position |
| `GetSize()` | Returns current (width, height) |
| `SetSize(width, height)` | Sets the window size |