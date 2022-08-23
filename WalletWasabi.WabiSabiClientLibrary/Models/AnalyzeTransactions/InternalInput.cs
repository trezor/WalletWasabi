using NBitcoin;

namespace WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

public record InternalInput(string PublicKey, Money Value)
{
}
