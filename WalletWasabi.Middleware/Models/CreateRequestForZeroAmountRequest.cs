using WalletWasabi.Crypto;

namespace WalletWasabi.Middleware.Models
{
	public record CreateRequestForZeroAmountRequest(
		CredentialIssuerParameters CredentialIssuerParameters
	);
}
