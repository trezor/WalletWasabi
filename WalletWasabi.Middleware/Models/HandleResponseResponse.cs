using WalletWasabi.Crypto.ZeroKnowledge;

namespace WalletWasabi.Middleware.Models
{
	public record HandleResponseResponse(
			Credential[] credentials
	);
}
