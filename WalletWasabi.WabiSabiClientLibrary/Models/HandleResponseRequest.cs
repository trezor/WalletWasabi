using WalletWasabi.Crypto;
using WalletWasabi.WabiSabi.Crypto.CredentialRequesting;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record HandleResponseRequest(
	CredentialIssuerParameters CredentialIssuerParameters,
	CredentialsResponse RegistrationResponse,
	CredentialsResponseValidation RegistrationValidationData
);
