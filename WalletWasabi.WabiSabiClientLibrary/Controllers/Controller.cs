using Microsoft.AspNetCore.Mvc;
using WalletWasabi.Crypto.Randomness;
using WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;
using WalletWasabi.WabiSabiClientLibrary.Models;
using WalletWasabi.WabiSabiClientLibrary.Filters;

namespace WalletWasabi.WabiSabiClientLibrary.Controllers;

[ApiController]
[ExceptionTranslateFilter]
[Produces("application/json")]
public class Controller : ControllerBase, IDisposable
{
	private SecureRandom _secureRandom;

	public Controller()
	{
		_secureRandom = new SecureRandom();
	}

	/// <summary>
	/// Given a set of effective input amounts registered by a participant and a set of effective input amounts
	/// registered by other participants, decompose the amounts registered by the participant into output amounts.
	/// </summary>
	[HttpPost("get-outputs-amounts")]
	public GetOutputAmountsResponse GetOutputAmounts(GetOutputAmountsRequest request)
	{
		return GetOutputAmountsHelper.GetOutputAmounts(request);
	}

	[HttpPost("get-zero-credential-requests")]
	public GetZeroCredentialRequestsResponse GetZeroCredentialRequests(GetZeroCredentialRequestsRequest request)
	{
		return CredentialHelper.GetZeroCredentialRequests(request, _secureRandom);
	}

	[HttpPost("get-real-credential-requests")]
	public GetRealCredentialRequestsResponse GetRealCredentialRequests(GetRealCredentialRequestsRequest request)
	{
		return CredentialHelper.GetRealCredentialRequests(request, _secureRandom);
	}

	[HttpPost("get-credentials")]
	public GetCredentialsResponse GetCredentials(GetCredentialsRequest request)
	{
		return CredentialHelper.GetCredentials(request, _secureRandom);
	}

	public void Dispose()
	{
	}
}
