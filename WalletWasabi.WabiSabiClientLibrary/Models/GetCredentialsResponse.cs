using WalletWasabi.Crypto.ZeroKnowledge;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record GetCredentialsResponse(
		Credential[] credentials
);
