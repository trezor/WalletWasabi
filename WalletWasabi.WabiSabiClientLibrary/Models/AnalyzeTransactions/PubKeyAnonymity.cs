using NBitcoin;

namespace WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

public record PubKeyAnonymity(string pubKey, long anonymitySet)
{
}
