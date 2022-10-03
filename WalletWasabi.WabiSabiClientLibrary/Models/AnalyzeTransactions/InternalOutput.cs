using NBitcoin;

namespace WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

public record InternalOutput(string Address, Money Value)
{
}
