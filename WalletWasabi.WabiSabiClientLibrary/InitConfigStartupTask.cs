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
		return Task.CompletedTask;
	}
}
