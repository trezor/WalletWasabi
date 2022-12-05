using WalletWasabi.WabiSabi.Crypto.CredentialRequesting;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record CreateZeroAmountCredentialRequestResponse(
	ZeroCredentialsRequestData zeroCredentialRequests
);
