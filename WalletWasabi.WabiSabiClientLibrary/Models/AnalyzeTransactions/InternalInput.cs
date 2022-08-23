using NBitcoin;

namespace WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

public record InternalInput(string Address, Money Value)
{
}
