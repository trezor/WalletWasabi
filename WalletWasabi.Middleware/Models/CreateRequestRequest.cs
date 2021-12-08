using WalletWasabi.Crypto.ZeroKnowledge;
using WalletWasabi.Crypto;

namespace WalletWasabi.Middleware.Models
{
	public record CreateRequestRequest(
		CredentialIssuerParameters CredentialIssuerParameters,
		long MaxCredentialValue,
		long[] AmountsToRequest,
		Credential[] CredentialsToPresent
	);
}
