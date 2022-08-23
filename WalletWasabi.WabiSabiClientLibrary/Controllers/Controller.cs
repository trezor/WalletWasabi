using Microsoft.AspNetCore.Mvc;
using WalletWasabi.Crypto.Randomness;
using WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;
using WalletWasabi.WabiSabiClientLibrary.Models;
using WalletWasabi.WabiSabi.Client;
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

	[HttpPost("get-anonymity-scores")]
	public GetAnonymityScoresResponse GetAnonymityScores(GetAnonymityScoresRequest request)
	{
		return GetAnonymityScoresHelper.GetAnonymityScores(request);
	}

	/// <summary>
	/// Given a set of unspent transaction outputs, choose a subset of the outputs that are best to register in a single CoinJoin round according to the given strategy.
	/// </summary>
	/// <seealso cref="CoinJoinClient.SelectCoinsForRound"/>
	[HttpPost("select-inputs-for-round")]
	public SelectInputsForRoundResponse SelectInputsForRound(SelectInputsForRoundRequest request)
	{
		return SelectInputsForRoundHelper.SelectInputsForRound(request);
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
