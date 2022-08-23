using NBitcoin;

namespace WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

public record InternalOutput(string PublicKey, Money Value, string ScriptPubKey)
{
}
