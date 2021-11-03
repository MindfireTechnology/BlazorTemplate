using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Web
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);

			builder.AddMisc();
			//builder.Services.AddAuthorizationCore();

			await builder.AddServicesAsync();

			builder.Services.AddOidcAuthentication(options =>
			{
				// Configure your authentication provider options here.
				// For more information, see https://aka.ms/blazor-standalone-auth
				builder.Configuration.Bind("Local", options.ProviderOptions);
			});

			

			await builder.Build().RunAsync();
		}
	}
}
