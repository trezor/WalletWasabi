using System.Linq;
using WalletWasabi.WabiSabiClientLibrary.Models;
using WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

namespace WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;

public class AnalyzeTransactionsHelper
{
	public static AnalyzeTransactionsResponse AnalyzeTransactions(AnalyzeTransactionsRequest request)
	{
		return new AnalyzeTransactionsResponse(request.Transactions.SelectMany(x => x.InternalOutputs).Select(x => x.Address).Distinct().Select(x => new AddressAnonymity(x, 0)).ToArray());
	}
}
