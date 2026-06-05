using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Blazor.Interops.Floating.Abstract;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Floating.Windows.Abstract;
using Soenneker.Blazor.Floating.Windows.Dtos;
using Soenneker.Blazor.Floating.Windows.Options;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using Soenneker.Utils.Json;

namespace Soenneker.Blazor.Floating.Windows;

///<inheritdoc cref="IFloatingWindowInterop"/>
public sealed class FloatingWindowInterop : IFloatingWindowInterop
{
    private const string _modulePath = "_content/Soenneker.Blazor.Floating.Windows/js/floatingwindowinterop.js";

    private readonly IResourceLoader _resourceLoader;
    private readonly IFloatingUiInterop _floatingUiInterop;
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly AsyncInitializer<bool> _scriptInitializer;

    private readonly CancellationScope _cancellationScope = new();

    public FloatingWindowInterop(IResourceLoader resourceLoader, IFloatingUiInterop floatingUiInterop,
        IModuleImportUtil moduleImportUtil)
    {
        _resourceLoader = resourceLoader;
        _floatingUiInterop = floatingUiInterop;
        _moduleImportUtil = moduleImportUtil;

        _scriptInitializer = new AsyncInitializer<bool>(InitializeScripts);
    }

    private async ValueTask InitializeScripts(bool useCdn, CancellationToken token)
    {
        await _floatingUiInterop.Initialize(useCdn, token);
        await _resourceLoader.LoadStyle("_content/Soenneker.Blazor.Floating.Windows/css/floatingwindow.css",
            cancellationToken: token);

        _ = await _moduleImportUtil.GetContentModuleReference(_modulePath, token);
    }

    public async ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _scriptInitializer.Init(useCdn, linked);
    }

    public async ValueTask Create(string id, FloatingWindowOptions options,
        CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            await _scriptInitializer.Init(options.UseCdn, linked);

            string json = JsonUtil.Serialize(options)!;

            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("create", linked, id, json);
        }
    }

    public async ValueTask SetCallbacks(string id, DotNetObjectReference<FloatingWindow> dotNetRef,
        CancellationToken cancellationToken = default)
    {
        IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, cancellationToken);
        await module.InvokeVoidAsync("setCallbacks", cancellationToken, id, dotNetRef);
    }

    public async ValueTask Destroy(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("destroy", linked, id);
        }
    }

    public async ValueTask Show(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("show", linked, id);
        }
    }

    public async ValueTask Hide(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("hide", linked, id);
        }
    }

    public async ValueTask Toggle(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("toggle", linked, id);
        }
    }

    public async ValueTask Close(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("close", linked, id);
        }
    }

    public async ValueTask<(int x, int y)> GetPosition(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<(int x, int y)>("getPosition", linked, id);
        }
    }

    public async ValueTask SetPosition(string id, int x, int y, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setPosition", linked, id, x, y);
        }
    }

    public async ValueTask<FloatingWindowSize> GetSize(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<FloatingWindowSize>("getSize", linked, id);
        }
    }

    public async ValueTask SetSize(string id, int width, int height, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setSize", linked, id, width, height);
        }
    }

    public async ValueTask BringToFront(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("bringToFront", linked, id);
        }
    }

    public async ValueTask<FloatingWindowSize> GetViewportSize(CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<FloatingWindowSize>("getViewportSize", linked);
        }
    }

    public async ValueTask CenterInViewport(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("centerInViewport", linked, id);
        }
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await _moduleImportUtil.DisposeContentModule(_modulePath);
        await _scriptInitializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}