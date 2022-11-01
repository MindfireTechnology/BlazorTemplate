using AutoMapper;
using Data;
using EFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mindfire.User;
using Mindfire.User.Implementation;
using WebApi.WeatherForecast.Services;
using WebApi;
using WebApi.WeatherForecast;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DatabaseContext>(config =>
{
	string? connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
	config.UseSqlServer(connectionString);
});

builder.Services.AddTransient<DbContext, DatabaseContext>();
builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient(typeof(Func<>));
builder.Services.AddTransient<IUserDataService, UserDataService>();
builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();

builder.Services.AddCors();

builder.Services.AddLogging(config =>
{
	config.AddConsole()
		.AddDebug();

	if (builder.Environment.IsDevelopment())
	{
		config.SetMinimumLevel(LogLevel.Debug);
	}
	else
	{
		config.SetMinimumLevel(LogLevel.Information);
	}

	config.AddFilter("Microsoft", LogLevel.Information)
		.AddFilter("System", LogLevel.Information);
});

var mapperConfig = new MapperConfiguration(cfg =>
{
	cfg.AddProfile(new ApiMapperConfiguration());
	cfg.AddProfile(new UserMapperConfiguration());
	cfg.AddProfile(new ForecastMapperConfiguration());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
}

app.UseCors(options =>
{
	options.AllowAnyOrigin();
	options.AllowAnyMethod();
	options.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
