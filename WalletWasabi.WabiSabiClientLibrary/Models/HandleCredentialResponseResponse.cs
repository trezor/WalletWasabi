using WalletWasabi.Crypto.ZeroKnowledge;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record HandleCredentialResponseResponse(
		Credential[] credentials
);
