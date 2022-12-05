using WalletWasabi.WabiSabi.Crypto.CredentialRequesting;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record GetZeroCredentialRequestsResponse(
	ZeroCredentialsRequestData zeroCredentialRequests
);
