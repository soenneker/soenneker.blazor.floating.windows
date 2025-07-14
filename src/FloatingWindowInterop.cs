using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Blazor.Floating.Windows.Abstract;
using Soenneker.Blazor.Floating.Windows.Dtos;
using Soenneker.Blazor.Floating.Windows.Options;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.AsyncSingleton;
using Soenneker.Utils.Json;

namespace Soenneker.Blazor.Floating.Windows;

///<inheritdoc cref="IFloatingWindowInterop"/>
public sealed class FloatingWindowInterop : IFloatingWindowInterop
{
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncSingleton _scriptInitializer;
    private readonly IJSRuntime _jSRuntime;

    private const string _module = "Soenneker.Blazor.Floating.Windows/js/floatingwindowinterop.js";
    private const string _moduleName = "FloatingWindowInterop";

    public FloatingWindowInterop(IJSRuntime jSRuntime, IResourceLoader resourceLoader)
    {
        _jSRuntime = jSRuntime;
        _resourceLoader = resourceLoader;

        _scriptInitializer = new AsyncSingleton(async (token, arg) =>
        {
            var useCdn = (bool) arg[0];

            if (useCdn)
            {
                await _resourceLoader.LoadScriptAndWaitForVariable("https://cdn.jsdelivr.net/npm/@floating-ui/core@1.7.2/dist/floating-ui.core.umd.min.js",
                                         "FloatingUICore", "sha256-OhWDdFHrIg8eNZaNgWL2ax7tjKNFOBQq3WErqxfHdlQ=", cancellationToken: token)
                                     .NoSync();

                await _resourceLoader.LoadScriptAndWaitForVariable("https://cdn.jsdelivr.net/npm/@floating-ui/dom@1.7.2/dist/floating-ui.dom.umd.min.js",
                                         "FloatingUIDOM", "sha256-cycZmidLw+l9uWDr4bUhL26YMJg1G6aM0AnUEPG9sME=", cancellationToken: token)
                                     .NoSync();
            }
            else
            {
                await _resourceLoader.LoadScriptAndWaitForVariable("_content/Soenneker.Blazor.Floating.Windows/js/floating-ui.core.umd.min.js",
                                         "FloatingUICore", cancellationToken: token)
                                     .NoSync();

                await _resourceLoader.LoadScriptAndWaitForVariable("_content/Soenneker.Blazor.Floating.Windows/js/floating-ui.dom.umd.min.js", "FloatingUIDOM",
                                         cancellationToken: token)
                                     .NoSync();
            }

            await _resourceLoader.LoadStyle("_content/Soenneker.Blazor.Floating.Windows/css/floatingwindow.css", cancellationToken: token).NoSync();
            await _resourceLoader.ImportModuleAndWaitUntilAvailable(_module, _moduleName, 100, token).NoSync();

            return new object();
        });
    }

    public ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default)
    {
        return _scriptInitializer.Init(cancellationToken, useCdn);
    }

    public async ValueTask Create(string id, FloatingWindowOptions options, CancellationToken cancellationToken = default)
    {
        await _scriptInitializer.Init(cancellationToken, options.UseCdn).NoSync();

        string json = JsonUtil.Serialize(options)!;

        await _jSRuntime.InvokeVoidAsync($"{_moduleName}.create", cancellationToken, id, json).NoSync();
    }

    public ValueTask SetCallbacks(string id, DotNetObjectReference<FloatingWindow> dotNetRef)
    {
        return _jSRuntime.InvokeVoidAsync($"{_moduleName}.setCallbacks", id, dotNetRef);
    }

    public ValueTask Destroy(string id, CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeVoidAsync($"{_moduleName}.destroy", cancellationToken, id);
    }

    public ValueTask Show(string id, CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeVoidAsync($"{_moduleName}.show", cancellationToken, id);
    }

    public ValueTask Hide(string id, CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeVoidAsync($"{_moduleName}.hide", cancellationToken, id);
    }

    public ValueTask Toggle(string id, CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeVoidAsync($"{_moduleName}.toggle", cancellationToken, id);
    }

    public ValueTask Close(string id, CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeVoidAsync($"{_moduleName}.close", cancellationToken, id);
    }

    public ValueTask<(int x, int y)> GetPosition(string id, CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeAsync<(int x, int y)>($"{_moduleName}.getPosition", cancellationToken, id);
    }

    public ValueTask SetPosition(string id, int x, int y, CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeVoidAsync($"{_moduleName}.setPosition", cancellationToken, id, x, y);
    }

    public ValueTask<FloatingWindowSize> GetSize(string id, CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeAsync<FloatingWindowSize>("FloatingWindowInterop.getSize", cancellationToken, id);
    }

    public ValueTask SetSize(string id, int width, int height, CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeVoidAsync($"{_moduleName}.setSize", cancellationToken, id, width, height);
    }

    public ValueTask BringToFront(string id, CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeVoidAsync($"{_moduleName}.bringToFront", cancellationToken, id);
    }

    public ValueTask<FloatingWindowSize> GetViewportSize(CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeAsync<FloatingWindowSize>("FloatingWindowInterop.getViewportSize", cancellationToken);
    }

    public ValueTask CenterInViewport(string id, CancellationToken cancellationToken = default)
    {
        return _jSRuntime.InvokeVoidAsync($"{_moduleName}.centerInViewport", cancellationToken, id);
    }

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_module).NoSync();
        await _scriptInitializer.DisposeAsync().NoSync();
    }
}
