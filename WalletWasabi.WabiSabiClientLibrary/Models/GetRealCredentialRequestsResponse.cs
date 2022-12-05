using WalletWasabi.WabiSabi.Crypto.CredentialRequesting;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record GetRealCredentialRequestsResponse(
	RealCredentialsRequestData realCredentialRequests
);
