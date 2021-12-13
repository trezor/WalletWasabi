using WalletWasabi.Crypto;
using WalletWasabi.WabiSabi.Crypto.CredentialRequesting;

namespace WalletWasabi.Middleware.Models
{
	public record HandleResponseRequest(
		CredentialIssuerParameters CredentialIssuerParameters,
		CredentialsResponse RegistrationResponse,
		CredentialsResponseValidation RegistrationValidationData
	);
}
