using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WalletWasabi.CoinJoin.Coordinator.Rounds;
using WalletWasabi.Helpers;
using WalletWasabi.Logging;
using WalletWasabi.WabiSabi.Backend;

namespace WalletWasabi.Middleware
{
	public class Global
	{
		public Global(string dataDir)
		{
			DataDir = dataDir ?? EnvironmentHelpers.GetDataDir(Path.Combine("WalletWasabi", "Middleware"));
			WabiSabiConfig = new(DataDir);
		}

		public string DataDir { get; }
		public WabiSabiConfig WabiSabiConfig { get; private set; }
	}
}
