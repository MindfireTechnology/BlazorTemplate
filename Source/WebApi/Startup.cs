using AutoMapper;
using Data;
using EFRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Mindfire.User;
using Mindfire.User.Implementation;
using System;

namespace WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		protected IConfiguration Configuration { get; }
		protected IHostEnvironment Environment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddDbContext<DatabaseContext>(config =>
			{
				string connectionString = Configuration.GetConnectionString("DatabaseConnection");
				config.UseSqlServer(connectionString);
			});

			services.AddTransient<DbContext, DatabaseContext>();
			services.AddTransient(typeof(IRepository), typeof(Repository));
			services.AddTransient(typeof(Func<>));
			services.AddTransient<IUserDataService, UserDataService>();

			services.AddCors();

			services.AddLogging(config =>
			{
				config.AddConsole()
					.AddDebug();

				if (Environment.IsDevelopment())
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
			});

			services.AddSingleton(mapperConfig.CreateMapper());

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
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

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
