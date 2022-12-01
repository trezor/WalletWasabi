using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using WalletWasabi.Logging;
using WalletWasabi.Server;

namespace WalletWasabi.WabiSabiClientLibrary;

public static class Program
{
	public static async Task Main(string[] args)
	{
		try
		{
			using var host = CreateHostBuilder(args).Build();
			await host.RunWithTasksAsync();
		}
		catch (Exception exception)
		{
			Logger.LogCritical(exception);
		}
	}

	public static IHostBuilder CreateHostBuilder(string[] args)
	{
		string? portString = Environment.GetEnvironmentVariable("WCL_BIND_PORT");
		int port = 37128;
		if (portString is not null)
		{
			port = Int32.Parse(portString);
		}

		return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => webBuilder
			.UseStartup<Startup>()
			.UseUrls($"http://localhost:{port}/"));
	}
}
