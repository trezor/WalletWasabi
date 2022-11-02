namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record GetVersionResponse(
	int version,
	string commitHash,
	bool release
);
