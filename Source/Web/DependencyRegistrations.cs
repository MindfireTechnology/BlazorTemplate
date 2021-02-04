using BlazorStateManager;
using BlazorStateManager.Mediator;
using BlazorStateManager.State;
using BlazorStateManager.StoragePersistance;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using System;
using System.Net.Http;

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

		public static WebAssemblyHostBuilder AddServices(this WebAssemblyHostBuilder builder)
		{
			var config = builder.Configuration;

			// HttpClient
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

			// MudBlazor Services
			builder.Services.AddMudBlazorDialog();
			builder.Services.AddMudBlazorResizeListener();
			builder.Services.AddMudBlazorSnackbar(config =>
			{
				config.PositionClass = Defaults.Classes.Position.BottomRight;

				config.PreventDuplicates = true;
				config.NewestOnTop = false;
				config.ShowCloseIcon = true;
				config.VisibleStateDuration = 5000;
				config.HideTransitionDuration = 500;
				config.ShowTransitionDuration = 500;
				config.SnackbarVariant = Variant.Filled;
			});

			builder.Services.RegisterStateManagerServices();

			return builder;
		}
	}
}
