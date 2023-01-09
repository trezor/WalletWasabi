using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;
using WalletWasabi.Logging;
using WalletWasabi.Server;
using WalletWasabi.WabiSabiClientLibrary.Middlewares;
using WalletWasabi.WabiSabi.Models.Serialization;
using System.IO;

[assembly: ApiController]

namespace WalletWasabi.WabiSabiClientLibrary;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	public void ConfigureServices(IServiceCollection services)
	{
#if (DEBUG)
		services.AddSwaggerGen(c =>
		{
			c.CustomSchemaIds(type => type.ToString());

			c.SwaggerDoc($"v{Global.Version}", new OpenApiInfo
			{
				Version = $"v{Global.Version}",
				Title = "WabiSabiClientLibrary API",
			});

			c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WalletWasabi.WabiSabiClientLibrary.xml"));
		});
#endif

		services.AddLogging(logging => logging.AddFilter((s, level) => level >= Microsoft.Extensions.Logging.LogLevel.Warning));

		services.AddControllers().AddNewtonsoftJson(x =>
		{
			x.SerializerSettings.Converters = JsonSerializationOptions.Default.Settings.Converters;
			x.SerializerSettings.ContractResolver = JsonSerializationOptions.Default.Settings.ContractResolver;
			x.SerializerSettings.MissingMemberHandling = JsonSerializationOptions.Default.Settings.MissingMemberHandling;
		});

		services.AddSingleton(new Global());
		services.AddStartupTask<InitConfigStartupTask>();

	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Global global)
	{
#if (DEBUG)
		app.UseMiddleware<RequestLoggerMiddleware>();

		app.UseSwagger();
		app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v{Global.Version}/swagger.json", $"WabiSabiClientLibrary API V{Global.Version}"));
#endif

		app.UseRouting();

		app.UseEndpoints(endpoints => endpoints.MapControllers());

		var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
		applicationLifetime.ApplicationStopped.Register(() => OnShutdown(global));
	}

	private void OnShutdown(Global global)
	{
		CleanupAsync(global).GetAwaiter().GetResult();
	}

	private Task CleanupAsync(Global global)
	{
		Logger.LogSoftwareStopped(nameof(WabiSabiClientLibrary));
		return Task.CompletedTask;
	}
}
