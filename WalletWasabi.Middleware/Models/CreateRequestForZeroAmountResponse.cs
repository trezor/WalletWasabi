using WalletWasabi.WabiSabi.Crypto.CredentialRequesting;

namespace WalletWasabi.Middleware.Models
{
	public record CreateRequestForZeroAmountResponse(
		ZeroCredentialsRequestData zeroCredentialsRequestData
	);
}
