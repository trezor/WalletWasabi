using System.Threading;
using Microsoft.AspNetCore.Mvc;
using NBitcoin;
using WalletWasabi.Server.Filters;
using WalletWasabi.Crypto.Randomness;
using WalletWasabi.WabiSabi.Crypto;
using WalletWasabi.Middleware.Models;

namespace WalletWasabi.Middleware
{
	[ApiController]
	[ExceptionTranslate]
	[Route("[controller]")]
	[Produces("application/json")]
	public class CryptographyController : ControllerBase
	{
		private SecureRandom SecureRandom;
		private long MaxAmountCredentialValue;

		public CryptographyController(Global global)
		{
			MaxAmountCredentialValue = global.WabiSabiConfig.MaxRegistrableAmount;
			SecureRandom = new SecureRandom();
		}


		[HttpPost("create-request")]
		public CreateRequestResponse CreateRequestAsync(CreateRequestRequest request)
		{
			WabiSabiClient wabiSabiClient = new WabiSabiClient(request.CredentialIssuerParameters, SecureRandom, request.MaxCredentialValue);
			var RealCredentialsRequestData = wabiSabiClient.CreateRequest(request.AmountsToRequest, request.CredentialsToPresent, CancellationToken.None);
			return new CreateRequestResponse(RealCredentialsRequestData);
		}
	}
}
