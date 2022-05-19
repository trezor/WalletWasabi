namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record GetAnonymityRequest(
	string Txid,
	int Vout
);
