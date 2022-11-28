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
