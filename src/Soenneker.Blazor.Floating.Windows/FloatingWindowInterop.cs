using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
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
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly AsyncInitializer<bool> _scriptInitializer;

    private readonly CancellationScope _cancellationScope = new();

    public FloatingWindowInterop(IResourceLoader resourceLoader, IModuleImportUtil moduleImportUtil)
    {
        _resourceLoader = resourceLoader;
        _moduleImportUtil = moduleImportUtil;

        _scriptInitializer = new AsyncInitializer<bool>(InitializeScripts);
    }

    private static string NormalizeContentUri(string uri)
    {
        if (string.IsNullOrEmpty(uri) || uri.Contains("://", StringComparison.Ordinal))
            return uri;

        return uri[0] == '/' ? uri : "/" + uri;
    }

    private async ValueTask InitializeScripts(bool useCdn, CancellationToken token)
    {
        if (useCdn)
        {
            await _resourceLoader.LoadScriptAndWaitForVariable(
                "https://cdn.jsdelivr.net/npm/@floating-ui/core@1.7.2/dist/floating-ui.core.umd.min.js",
                "FloatingUICore",
                "sha256-OhWDdFHrIg8eNZaNgWL2ax7tjKNFOBQq3WErqxfHdlQ=",
                cancellationToken: token);
            await _resourceLoader.LoadScriptAndWaitForVariable(
                "https://cdn.jsdelivr.net/npm/@floating-ui/dom@1.7.2/dist/floating-ui.dom.umd.min.js",
                "FloatingUIDOM",
                "sha256-cycZmidLw+l9uWDr4bUhL26YMJg1G6aM0AnUEPG9sME=",
                cancellationToken: token);
        }
        else
        {
            await _resourceLoader.LoadScriptAndWaitForVariable(
                NormalizeContentUri("_content/Soenneker.Blazor.Floating.Windows/js/floating-ui.core.umd.min.js"),
                "FloatingUICore",
                cancellationToken: token);
            await _resourceLoader.LoadScriptAndWaitForVariable(
                NormalizeContentUri("_content/Soenneker.Blazor.Floating.Windows/js/floating-ui.dom.umd.min.js"),
                "FloatingUIDOM",
                cancellationToken: token);
        }

        await _resourceLoader.LoadStyle(NormalizeContentUri("_content/Soenneker.Blazor.Floating.Windows/css/floatingwindow.css"), cancellationToken: token);

        _ = await _moduleImportUtil.GetContentModuleReference(_modulePath, token);
    }

    public async ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _scriptInitializer.Init(useCdn, linked);
    }

    public async ValueTask Create(string id, FloatingWindowOptions options, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            await _scriptInitializer.Init(options.UseCdn, linked);

            string json = JsonUtil.Serialize(options)!;

            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("create", linked, id, json);
        }
    }

    public async ValueTask SetCallbacks(string id, DotNetObjectReference<FloatingWindow> dotNetRef)
    {
        IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, CancellationToken.None);
        await module.InvokeVoidAsync("setCallbacks", CancellationToken.None, id, dotNetRef);
    }

    public async ValueTask Destroy(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("destroy", linked, id);
        }
    }

    public async ValueTask Show(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("show", linked, id);
        }
    }

    public async ValueTask Hide(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("hide", linked, id);
        }
    }

    public async ValueTask Toggle(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("toggle", linked, id);
        }
    }

    public async ValueTask Close(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("close", linked, id);
        }
    }

    public async ValueTask<(int x, int y)> GetPosition(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<(int x, int y)>("getPosition", linked, id);
        }
    }

    public async ValueTask SetPosition(string id, int x, int y, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setPosition", linked, id, x, y);
        }
    }

    public async ValueTask<FloatingWindowSize> GetSize(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<FloatingWindowSize>("getSize", linked, id);
        }
    }

    public async ValueTask SetSize(string id, int width, int height, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("setSize", linked, id, width, height);
        }
    }

    public async ValueTask BringToFront(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("bringToFront", linked, id);
        }
    }

    public async ValueTask<FloatingWindowSize> GetViewportSize(CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<FloatingWindowSize>("getViewportSize", linked);
        }
    }

    public async ValueTask CenterInViewport(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("centerInViewport", linked, id);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _moduleImportUtil.DisposeContentModule(_modulePath);
        await _scriptInitializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
