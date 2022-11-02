namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record GetVersionResponse(
	string version,
	string commitHash,
	bool debug
);
