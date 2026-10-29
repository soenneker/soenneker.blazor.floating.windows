using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;
using System.Text.Json;
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

/// <inheritdoc cref="IFloatingWindowInterop"/>
public sealed class FloatingWindowInterop : IFloatingWindowInterop
{
    private readonly JsonSerializerOptions _jsonOptions;

    private JsonTypeInfo<T> GetJsonTypeInfo<T>() => (JsonTypeInfo<T>)_jsonOptions.GetTypeInfo(typeof(T));



    private const string _modulePath = "_content/Soenneker.Blazor.Floating.Windows/js/floatingwindowinterop.js";

    private readonly IResourceLoader _resourceLoader;
    private readonly IFloatingUiInterop _floatingUiInterop;
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly AsyncInitializer<bool> _scriptInitializer;

    private readonly CancellationScope _cancellationScope = new();

    public FloatingWindowInterop(IResourceLoader resourceLoader, IFloatingUiInterop floatingUiInterop,
        IModuleImportUtil moduleImportUtil, JsonSerializerContext? jsonContext = null)
    {
        _jsonOptions = LibraryJsonContext.WithContext(jsonContext);
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
            try
            {
                await _scriptInitializer.Init(options.UseCdn, linked);

                string json = JsonUtil.Serialize(options, GetJsonTypeInfo<FloatingWindowOptions>())!;

                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                await module.InvokeVoidAsync("create", linked, id, json);
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.Create' failed: {exception.Message}", exception);
            }
        }
    }

    public async ValueTask SetCallbacks(string id, DotNetObjectReference<FloatingWindow> dotNetRef,
        CancellationToken cancellationToken = default)
    {
        try
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, cancellationToken);
            await module.InvokeVoidAsync("setCallbacks", cancellationToken, id, dotNetRef);
        }
        catch (JSException exception)
        {
            throw new JSException($"JavaScript operation 'FloatingWindowInterop.SetCallbacks' failed: {exception.Message}", exception);
        }
    }

    public async ValueTask Destroy(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                await module.InvokeVoidAsync("destroy", linked, id);
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.Destroy' failed: {exception.Message}", exception);
            }
        }
    }

    public async ValueTask Show(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                await module.InvokeVoidAsync("show", linked, id);
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.Show' failed: {exception.Message}", exception);
            }
        }
    }

    public async ValueTask Hide(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                await module.InvokeVoidAsync("hide", linked, id);
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.Hide' failed: {exception.Message}", exception);
            }
        }
    }

    public async ValueTask Toggle(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                await module.InvokeVoidAsync("toggle", linked, id);
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.Toggle' failed: {exception.Message}", exception);
            }
        }
    }

    public async ValueTask Close(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                await module.InvokeVoidAsync("close", linked, id);
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.Close' failed: {exception.Message}", exception);
            }
        }
    }

    public async ValueTask<(int x, int y)> GetPosition(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                JsonElement payload = await module.InvokeAsync<JsonElement>("getPosition", linked, id);
                FloatingWindowPosition? position = payload.Deserialize(GetJsonTypeInfo<FloatingWindowPosition>());
                return position is null ? default : (position.X, position.Y);
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.GetPosition' failed: {exception.Message}", exception);
            }
        }
    }

    public async ValueTask SetPosition(string id, int x, int y, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                await module.InvokeVoidAsync("setPosition", linked, id, x, y);
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.SetPosition' failed: {exception.Message}", exception);
            }
        }
    }

    public async ValueTask<FloatingWindowSize> GetSize(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                JsonElement payload = await module.InvokeAsync<JsonElement>("getSize", linked, id);
                return payload.Deserialize(GetJsonTypeInfo<FloatingWindowSize>())!;
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.GetSize' failed: {exception.Message}", exception);
            }
        }
    }

    public async ValueTask SetSize(string id, int width, int height, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                await module.InvokeVoidAsync("setSize", linked, id, width, height);
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.SetSize' failed: {exception.Message}", exception);
            }
        }
    }

    public async ValueTask BringToFront(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                await module.InvokeVoidAsync("bringToFront", linked, id);
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.BringToFront' failed: {exception.Message}", exception);
            }
        }
    }

    public async ValueTask<FloatingWindowSize> GetViewportSize(CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                JsonElement payload = await module.InvokeAsync<JsonElement>("getViewportSize", linked);
                return payload.Deserialize(GetJsonTypeInfo<FloatingWindowSize>())!;
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.GetViewportSize' failed: {exception.Message}", exception);
            }
        }
    }

    public async ValueTask CenterInViewport(string id, CancellationToken cancellationToken = default)
    {
        CancellationToken linked =
            _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            try
            {
                IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
                await module.InvokeVoidAsync("centerInViewport", linked, id);
            }
            catch (JSException exception)
            {
                throw new JSException($"JavaScript operation 'FloatingWindowInterop.CenterInViewport' failed: {exception.Message}", exception);
            }
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