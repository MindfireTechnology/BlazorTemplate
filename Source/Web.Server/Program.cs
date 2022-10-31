using BlazorStateManager.Mediator;
using BlazorStateManager.State;
using BlazorStateManager.StoragePersistance;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using MudBlazor;
using MudBlazor.Services;
using Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<IMediator, BlazorMediator>();
builder.Services.AddScoped<IStoragePersistance, SessionStoragePersistance>();
builder.Services.AddScoped<IStateManager, StateManager>();

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

// register an HttpClient that points to itself
builder.Services.AddSingleton(sp =>
{
	// Get the address that the app is currently running at
	var server = sp.GetRequiredService<IServer>();
	var addressFeature = server.Features.Get<IServerAddressesFeature>();
	string baseAddress = addressFeature.Addresses.First();
	return new HttpClient { BaseAddress = new Uri(baseAddress) };
});

var clientSettings = new ClientSettings();
builder.Configuration.GetSection(nameof(ClientSettings)).Bind(clientSettings);
builder.Services.AddSingleton(clientSettings);

var app = builder.Build();

if (app.Environment.IsDevelopment())
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

app.Run();
