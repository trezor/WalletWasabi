using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WalletWasabi.Crypto.Randomness;
using WalletWasabi.Crypto.ZeroKnowledge;
using WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;
using WalletWasabi.WabiSabiClientLibrary.Models;
using WalletWasabi.Server.Filters;
using WalletWasabi.WabiSabi.Crypto;
using WalletWasabi.WabiSabi.Crypto.CredentialRequesting;


namespace WalletWasabi.WabiSabiClientLibrary;

[ApiController]
[ExceptionTranslate]
[Route("[controller]")]
[Produces("application/json")]
public class CryptographyController : ControllerBase, IDisposable
{
	private SecureRandom _secureRandom;

	public CryptographyController(Global global)
	{
		MaxAmountCredentialValue = global.WabiSabiConfig.MaxRegistrableAmount;
		_secureRandom = new SecureRandom();
	}

	private long MaxAmountCredentialValue { get; }

	/// <summary>
	/// Given a set of effective input amounts registered by a participant and a set of effective input amounts
	/// registered by other participants, decompose the amounts registered by the participant into output amounts. 
	/// </summary>
	[HttpPost("decompose-amounts")]
	public DecomposeAmountsResponse DecomposeAmounts(DecomposeAmountsRequest request)
	{
		return DecomposeAmountsHelper.Decompose(request);
	}

	[HttpPost("create-request-for-zero-amount")]
	public CreateRequestForZeroAmountResponse CreateRequestForZeroAmountAsync(CreateRequestForZeroAmountRequest request)
	{
		WabiSabiClient wabiSabiClient = new(request.CredentialIssuerParameters, _secureRandom, MaxAmountCredentialValue);
		ZeroCredentialsRequestData requestData = wabiSabiClient.CreateRequestForZeroAmount();
		return new CreateRequestForZeroAmountResponse(requestData);
	}

	[HttpPost("create-request")]
	public CreateRequestResponse CreateRequestAsync(CreateRequestRequest request)
	{
		WabiSabiClient wabiSabiClient = new(request.CredentialIssuerParameters, _secureRandom, request.MaxCredentialValue);
		RealCredentialsRequestData requestData = wabiSabiClient.CreateRequest(request.AmountsToRequest, request.CredentialsToPresent, CancellationToken.None);
		return new CreateRequestResponse(requestData);
	}

	[HttpPost("handle-response")]
	public HandleResponseResponse CreateRequestAsync(HandleResponseRequest request)
	{
		WabiSabiClient wabiSabiClient = new(request.CredentialIssuerParameters, _secureRandom, MaxAmountCredentialValue);
		IEnumerable<Credential> credentials = wabiSabiClient.HandleResponse(request.RegistrationResponse, request.RegistrationValidationData);
		return new HandleResponseResponse(credentials.ToArray());
	}

	public void Dispose()
	{
		_secureRandom.Dispose();
	}
}
