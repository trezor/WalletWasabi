using Microsoft.AspNetCore.Hosting;
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
		Logger.SetModes(LogMode.Console);
		Logger.SetMinimumLevel(Logging.LogLevel.Info);
		Logger.LogSoftwareStarted(nameof(WabiSabiClientLibrary));
		return Task.CompletedTask;
	}

	private static void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
	{
		Logger.LogWarning(unobservedTaskExceptionEventArgs.Exception);
	}

	private static void CurrentDomain_UnhandledException(object? sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
	{
		if (unhandledExceptionEventArgs.ExceptionObject is Exception exception)
		{
			Logger.LogWarning(exception);
		}
	}
}
