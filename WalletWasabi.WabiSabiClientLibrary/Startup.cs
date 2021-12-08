using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using WalletWasabi.Server;

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
		services.AddSingleton(new Global());
		services.AddStartupTask<InitConfigStartupTask>();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Global global)
	{
		var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
		applicationLifetime.ApplicationStopped.Register(() => OnShutdown(global));
	}

	private void OnShutdown(Global global)
	{
		CleanupAsync(global).GetAwaiter().GetResult();
	}

	private Task CleanupAsync(Global global)
	{
		return Task.CompletedTask;
	}
}
