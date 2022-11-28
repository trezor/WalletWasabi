using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WalletWasabi.Logging;
using WalletWasabi.Server;
using WalletWasabi.WabiSabi.Models.Serialization;

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
		services.AddLogging(logging => logging.AddFilter((s, level) => level >= Microsoft.Extensions.Logging.LogLevel.Warning));

		services.AddControllers().AddNewtonsoftJson(x =>
		{
			x.SerializerSettings.Converters = JsonSerializationOptions.Default.Settings.Converters;
		});

		services.AddSingleton(new Global());
		services.AddStartupTask<InitConfigStartupTask>();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Global global)
	{
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
