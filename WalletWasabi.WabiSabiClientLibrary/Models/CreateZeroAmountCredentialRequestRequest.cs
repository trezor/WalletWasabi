using WalletWasabi.Crypto;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record CreateZeroAmountCredentialRequestRequest(
	long MaxAmountCredentialValue,
	CredentialIssuerParameters CredentialIssuerParameters
);
