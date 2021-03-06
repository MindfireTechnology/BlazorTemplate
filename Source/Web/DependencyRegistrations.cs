using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Web
{
	public static class DependencyRegistrations
	{
		public static WebAssemblyHostBuilder AddMisc(this WebAssemblyHostBuilder builder)
		{
			builder.Services.AddTransient(typeof(Lazy<>));
			builder.Services.AddTransient(typeof(Func<>));

			return builder;
		}

		public static async Task<WebAssemblyHostBuilder> AddServicesAsync(this WebAssemblyHostBuilder builder)
		{
			var config = builder.Configuration;

			var settingsClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

			// get the client settings to use with the api
			var settings = await settingsClient.GetFromJsonAsync<ClientSettings>("clientSettings.json");
			builder.Services.AddSingleton(settings);

			// HttpClient
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(settings.ApiUrl) });

			// MudBlazor Services
			builder.Services.AddMudServices(config =>
			{
				config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

				config.SnackbarConfiguration.PreventDuplicates = true;
				config.SnackbarConfiguration.NewestOnTop = false;
				config.SnackbarConfiguration.ShowCloseIcon = true;
				config.SnackbarConfiguration.VisibleStateDuration = 5000;
				config.SnackbarConfiguration.HideTransitionDuration = 500;
				config.SnackbarConfiguration.ShowTransitionDuration = 500;
				config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
			});

			builder.Services.RegisterStateManagerServices();

			return builder;
		}
	}
}
