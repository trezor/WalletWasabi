using NBitcoin;

namespace WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

public record ExternalOutput(Money Value, string ScriptPubKey)
{
}
