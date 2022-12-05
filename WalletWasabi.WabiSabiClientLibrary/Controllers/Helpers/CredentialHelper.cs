using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WalletWasabi.Crypto.Randomness;
using WalletWasabi.Crypto.ZeroKnowledge;
using WalletWasabi.WabiSabi.Crypto;
using WalletWasabi.WabiSabi.Crypto.CredentialRequesting;
using WalletWasabi.WabiSabiClientLibrary.Models;

namespace WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;

public class CredentialHelper
{
	public static CreateCredentialRequestResponse CreateCredentialRequest(CreateCredentialRequestRequest request, SecureRandom secureRandom)
	{
		WabiSabiClient wabiSabiClient = new(request.CredentialIssuerParameters, secureRandom, request.MaxCredentialValue);
		RealCredentialsRequestData requestData = wabiSabiClient.CreateRequest(request.AmountsToRequest, request.CredentialsToPresent, CancellationToken.None);
		return new CreateCredentialRequestResponse(requestData);
	}

	public static CreateZeroAmountCredentialRequestResponse CreateZeroAmountCredentialRequest(CreateZeroAmountCredentialRequestRequest request, SecureRandom secureRandom)
	{
		WabiSabiClient wabiSabiClient = new(request.CredentialIssuerParameters, secureRandom, request.MaxAmountCredentialValue);
		ZeroCredentialsRequestData requestData = wabiSabiClient.CreateRequestForZeroAmount();
		return new CreateZeroAmountCredentialRequestResponse(requestData);
	}

	public static HandleCredentialResponseResponse HandleCredentialResponse(HandleCredentialResponseRequest request, SecureRandom secureRandom)
	{
		WabiSabiClient wabiSabiClient = new(request.CredentialIssuerParameters, secureRandom, request.MaxAmountCredentialValue);
		IEnumerable<Credential> credentials = wabiSabiClient.HandleResponse(request.CredentialsResponse, request.CredentialsValidationData);
		return new HandleCredentialResponseResponse(credentials.ToArray());
	}
}
