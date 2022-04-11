using System.IO;
using WalletWasabi.Helpers;
using WalletWasabi.WabiSabi.Backend;

namespace WalletWasabi.WabiSabiClientLibrary;

public class Global
{
	public Global(string dataDir)
	{
		DataDir = dataDir ?? EnvironmentHelpers.GetDataDir(Path.Combine(nameof(WalletWasabi), nameof(WabiSabiClientLibrary)));
		WabiSabiConfig = new(DataDir);
	}

	public string DataDir { get; }
	public WabiSabiConfig WabiSabiConfig { get; private set; }
}
