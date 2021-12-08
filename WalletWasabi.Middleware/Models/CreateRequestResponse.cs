using WalletWasabi.WabiSabi.Crypto.CredentialRequesting;

namespace WalletWasabi.Middleware.Models
{
	public record CreateRequestResponse(
		RealCredentialsRequestData realCredentialsRequestData
	);
}
