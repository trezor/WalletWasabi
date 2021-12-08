using System.Threading;
using System.Threading.Tasks;

namespace WalletWasabi.Server;

public interface IStartupTask
{
	Task ExecuteAsync(CancellationToken cancellationToken = default);
}
