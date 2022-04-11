using System.IO;
using WalletWasabi.Helpers;
using WalletWasabi.WabiSabi.Backend;

namespace WalletWasabi.Middleware;

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
