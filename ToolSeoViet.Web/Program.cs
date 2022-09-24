using Blazored.LocalStorage;
using Blazored.LocalStorage.Serialization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ToolSeoViet.Web.Services;
using ToolSeoViet.Web.Services.Common;
using ToolSeoViet.Web.Services.ProjectService;
using ToolSeoViet.Web.Services.SeoServices;
using TuanVu.Management.Web;
using TuanVu.Management.Web.Utilities.Helpers;

namespace ToolSeoViet.Web {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddMudServices(config => {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;

                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = true;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 4000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            builder.Services.AddBlazoredLocalStorage()
                .Replace(ServiceDescriptor.Scoped<IJsonSerializer, CustomJsonSerializer>());

            builder.Services.AddScoped(sp => new HttpClient {
                BaseAddress = new Uri(builder.Configuration["ApiUrl"]),
                Timeout = TimeSpan.FromSeconds(60),
            });
            builder.Services.AddOptions();
            builder.Services.AddScoped<HttpService>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();

            builder.Services.AddApiAuthorization();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<SeoService>();
            builder.Services.AddScoped<ProjectService>();


            var host = builder.Build();

            var authService = host.Services.GetRequiredService<AuthService>();
            await authService.Initialize();

            await host.RunAsync();
        }
    }
}
