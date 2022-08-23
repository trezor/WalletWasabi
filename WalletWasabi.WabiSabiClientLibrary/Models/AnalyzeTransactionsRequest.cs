using WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record AnalyzeTransactionsRequest(
    Tx[] Transactions
);
