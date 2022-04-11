using WalletWasabi.Crypto.ZeroKnowledge;
using WalletWasabi.Crypto;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record CreateRequestRequest(
	CredentialIssuerParameters CredentialIssuerParameters,
	long MaxCredentialValue,
	long[] AmountsToRequest,
	Credential[] CredentialsToPresent
);
