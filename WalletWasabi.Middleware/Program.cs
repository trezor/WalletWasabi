using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using WalletWasabi.Server;
using WalletWasabi.Logging;

namespace WalletWasabi.Middleware
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			try
			{
				using var host = CreateHostBuilder(args).Build();
				await host.RunWithTasksAsync();
			}
			catch (Exception ex)
			{
				Logger.LogCritical(ex);
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => webBuilder
				.UseStartup<Startup>()
				.UseUrls("http://localhost:37128/"));
	}
}
