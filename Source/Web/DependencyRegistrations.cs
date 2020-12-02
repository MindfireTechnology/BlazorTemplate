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
			builder.Services.AddMudBlazorSnackbar();
			builder.Services.AddMudBlazorResizeListener();

			return builder;
		}
	}
}
