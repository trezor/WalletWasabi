using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WalletWasabi.Logging;
using WalletWasabi.Server;

namespace WalletWasabi.WabiSabiClientLibrary;

public class InitConfigStartupTask : IStartupTask
{
	public InitConfigStartupTask(Global global, IWebHostEnvironment hostingEnvironment)
	{
		Global = global;
	}

	public Global Global { get; }

	public Task ExecuteAsync(CancellationToken cancellationToken)
	{
		Logger.InitializeDefaults(Path.Combine(Global.DataDir, "Logs.txt"));
		Logger.LogSoftwareStarted(nameof(WabiSabiClientLibrary));
		return Task.CompletedTask;
	}

	private static void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
	{
		Logger.LogWarning(e.Exception);
	}

	private static void CurrentDomain_UnhandledException(object? sender, UnhandledExceptionEventArgs e)
	{
		if (e.ExceptionObject is Exception ex)
		{
			Logger.LogWarning(ex);
		}
	}
}
