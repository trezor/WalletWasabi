using WalletWasabi.Crypto;
using WalletWasabi.WabiSabi.Crypto.CredentialRequesting;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record HandleCredentialResponseRequest(
	long MaxAmountCredentialValue,
	CredentialIssuerParameters CredentialIssuerParameters,
	CredentialsResponse CredentialsResponse,
	CredentialsResponseValidation CredentialsValidationData
);
