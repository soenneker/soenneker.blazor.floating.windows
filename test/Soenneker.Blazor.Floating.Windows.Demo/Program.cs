﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Serilog;
using Serilog.Debugging;
using Soenneker.Blazor.Floating.Windows.Registrars;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using Soenneker.Serilog.Sinks.Browser.Blazor.Registrars;

namespace Soenneker.Blazor.Floating.Windows.Demo;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            ConfigureLogging(builder.Services);

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            builder.Services.AddFloatingWindowAsScoped();

            builder.Services
                .AddBlazorise()
                .AddBootstrapProviders();

            WebAssemblyHost host = builder.Build();

            var jsRuntime = (IJSRuntime) host.Services.GetService(typeof(IJSRuntime))!;

            SetGlobalLogger(jsRuntime);

            await host.RunAsync().NoSync();
        }
        catch (Exception e)
        {
            Log.Error(e, "Stopped program because of exception");
            throw;
        }
        finally
        {
            await Log.CloseAndFlushAsync().NoSync();
        }
    }

    private static void ConfigureLogging(IServiceCollection services)
    {
        SelfLog.Enable(m => Console.Error.WriteLine(m));

        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddFilter("Microsoft.AspNetCore.Components.RenderTree.*", LogLevel.None);

            builder.AddSerilog(dispose: true);
        });
    }

    private static void SetGlobalLogger(IJSRuntime jsRuntime)
    {
        var loggerConfig = new LoggerConfiguration();

        loggerConfig.WriteTo.BlazorConsole(jsRuntime: jsRuntime);

        Log.Logger = loggerConfig.CreateLogger();
    }
}
