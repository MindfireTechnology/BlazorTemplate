using BlazorStateManager.Mediator;
using BlazorStateManager.State;
using BlazorStateManager.StoragePersistance;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Refit;
using Web.WeatherForecast;
using WebApi.Models;
using Web.WeatherForecast.Services;
using Web.User;
using Web.User.Services;

namespace Web;

public static class DependencyRegistrations
{
	public static WebAssemblyHostBuilder AddMisc(this WebAssemblyHostBuilder builder)
	{
		builder.Services.AddTransient(typeof(Lazy<>));
		builder.Services.AddTransient(typeof(Func<>));

		return builder;
	}

	public static WebAssemblyHostBuilder AddServices(this WebAssemblyHostBuilder builder)
	{
		var config = builder.Configuration;

		var clientSettings = new ClientSettings();
		builder.Configuration.GetSection(nameof(ClientSettings)).Bind(clientSettings);
		builder.Services.AddSingleton(clientSettings);

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

		builder.Services.AddSingleton<IMediator, BlazorMediator>();
		builder.Services.AddScoped<IStateManager, StateManager>();
		builder.Services.AddScoped<IStoragePersistance, LocalStoragePersistance>();

		builder.Services.AddRefitClient<IWeatherForecastClient>()
			.ConfigureHttpClient(client =>
			{
				client.BaseAddress = new Uri(clientSettings.ApiUrl!);
				client.Timeout = TimeSpan.FromSeconds(clientSettings.Timeout ?? 30);
			});

		builder.Services.AddRefitClient<IUserClient>()
			.ConfigureHttpClient(client =>
			{
				client.BaseAddress = new Uri(clientSettings.ApiUrl!);
				client.Timeout = TimeSpan.FromSeconds(clientSettings.Timeout ?? 30);
			});

		builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();
		builder.Services.AddTransient<IUserService, UserService>();

		builder.Services.AddLogging();

		return builder;
	}
}
