using BlazorStateManager.Mediator;
using BlazorStateManager.State;
using BlazorStateManager.StoragePersistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Server
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			services.AddServerSideBlazor();
			//services.RegisterStateManagerServices();

			services.AddSingleton<IMediator, BlazorMediator>();
			services.AddScoped<IStoragePersistance, SessionStoragePersistance>();
			//services.AddSingleton<IStoragePersistance, CookieStoragePersistance>();
			services.AddScoped<IStateManager, StateManager>();

			services.AddMudServices(config =>
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


			// register an HttpClient that points to itself
			services.AddSingleton<HttpClient>(sp =>
			{
				// Get the address that the app is currently running at
				var server = sp.GetRequiredService<IServer>();
				var addressFeature = server.Features.Get<IServerAddressesFeature>();
				string baseAddress = addressFeature.Addresses.First();
				return new HttpClient { BaseAddress = new Uri(baseAddress) };
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseStatusCodePages();
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapFallbackToPage("/Default");
			});
		}
	}
}
