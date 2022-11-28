using Microsoft.AspNetCore.Mvc;
using WalletWasabi.Crypto.Randomness;
using WalletWasabi.WabiSabiClientLibrary.Models;
using WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;
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

	[HttpPost("create-zero-amount-credential-request")]
	public CreateZeroAmountCredentialRequestResponse CreateZeroAmountCredentialRequestAsync(CreateZeroAmountCredentialRequestRequest request)
	{
		return CredentialHelper.CreateZeroAmountCredentialRequest(request, _secureRandom);
	}

	[HttpPost("create-credential-request")]
	public CreateCredentialRequestResponse CreateCredentialRequestAsync(CreateCredentialRequestRequest request)
	{
		return CredentialHelper.CreateCredentialRequest(request, _secureRandom);
	}

	[HttpPost("handle-credential-response")]
	public HandleCredentialResponseResponse HandleCredentialResponseAsync(HandleCredentialResponseRequest request)
	{
		return CredentialHelper.HandleCredentialResponse(request, _secureRandom);
	}

	public void Dispose()
	{
	}
}
