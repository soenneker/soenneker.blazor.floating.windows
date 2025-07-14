[![](https://img.shields.io/nuget/v/soenneker.blazor.floating.windows.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.floating.windows/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.floating.windows/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.floating.windows/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.floating.windows.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.floating.windows/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.blazor.floating.windows/)

# <img src="https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png" alt="Logo" width="48"/> Soenneker.Blazor.Floating.Windows

> **Modern, customizable floating windows for Blazor**

---


## ✨ Features

- 🪟 **Draggable & Resizable** — Move and resize windows with smooth, modern UX
- 🎨 **Fully Customizable** — Colors, fonts, borders, shadows, and more
- 🌗 **Custom theming** — Built-in typed support
- 🖼️ **Multiple Windows** — Manage, stack, and focus with z-index
- 📱 **Responsive** — Works on desktop and mobile
- ♿ **Accessible** — ARIA labels, keyboard navigation
- ⚡ **Event Callbacks** — React to show, hide, drag, resize, and more

---

## 🚀 Installation

```bash
dotnet add package Soenneker.Blazor.Floating.Windows
```

```csharp
// Program.cs
using Soenneker.Blazor.Floating.Windows.Registrars;
builder.Services.AddFloatingWindowAsScoped();
```

```razor
// _Imports.razor
@using Soenneker.Blazor.Floating.Windows
```

---

## 🛠️ Basic Usage

```razor
<FloatingWindow Title="My Window" Width="400" Height="300">
    <div>
        <h3>Hello World!</h3>
        <p>This is a floating window with your content.</p>
        <button @onclick="() => window?.Close()">Close</button>
    </div>
</FloatingWindow>
```

---

## 🎨 Theming & Styling

**Built-in themes:**
```razor
<FloatingWindow StyleOptions="FloatingWindowStyleOptions.DefaultLight">...</FloatingWindow>
<FloatingWindow StyleOptions="FloatingWindowStyleOptions.DefaultDark">...</FloatingWindow>
<FloatingWindow StyleOptions="FloatingWindowStyleOptions.DemoTheme">...</FloatingWindow>
```

**Custom styling:**
```razor
<FloatingWindow StyleOptions="new FloatingWindowStyleOptions
{
    BackgroundColor = "#f8f9fa",
    BorderColor = "#dee2e6",
    BorderRadius = "12px",
    TitleBarBackgroundColor = "#007bff",
    TitleBarTextColor = "#ffffff",
    CloseButtonTextColor = "#ffffff",
    CloseButtonHoverBackgroundColor = "rgba(255, 255, 255, 0.2)",
    CustomClass = "my-custom-window",
    CustomStyles = new Dictionary<string, string>
    {
        ["backdrop-filter"] = "blur(10px)",
        ["border"] = "2px solid #007bff"
    }
}">...</FloatingWindow>
```

---

## 💡 Advanced Example

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
    private void OnWindowShow() => Console.WriteLine("Window shown!");
    private void OnWindowHide() => Console.WriteLine("Window hidden!");
    private void OnDragStart() => Console.WriteLine("Dragging started!");
    private void OnDragEnd() => Console.WriteLine("Dragging ended!");
}
```

---

## ⚙️ API Overview

### Properties
| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Title` | `string` | `"Window"` | Title bar text |
| `Width` | `int?` | `400` | Initial width (px) |
| `Height` | `int?` | `300` | Initial height (px) |
| `InitialX` | `int?` | `100` | Initial X position |
| `InitialY` | `int?` | `100` | Initial Y position |
| `Draggable` | `bool?` | `true` | Enable dragging |
| `Resizable` | `bool?` | `true` | Enable resizing |
| `ShowCloseButton` | `bool?` | `true` | Show close button |
| `ShowTitleBar` | `bool?` | `true` | Show title bar |
| `StyleOptions` | `FloatingWindowStyleOptions` | `DefaultLight` | Styling config |

### Events
| Event | Description |
|-------|-------------|
| `OnShow` | Window shown |
| `OnHide` | Window hidden |
| `OnDragStart` | Dragging started |
| `OnDragEnd` | Dragging ended |

### Methods
| Method | Description |
|--------|-------------|
| `Show()` | Show window |
| `Hide()` | Hide window |
| `Toggle()` | Toggle visibility |
| `GetPosition()` | Get (x, y) |
| `SetPosition(x, y)` | Set position |
| `GetSize()` | Get (width, height) |
| `SetSize(width, height)` | Set size |
| `BringToFront()` | Bring window to front |
