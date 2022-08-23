using WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record AnalyzeTransactionsResponse(
	PubKeyAnonymity[] Results
);
