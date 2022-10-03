using NBitcoin;

namespace WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

public record AddressAnonymity(string address, long anonymitySet)
{
}
