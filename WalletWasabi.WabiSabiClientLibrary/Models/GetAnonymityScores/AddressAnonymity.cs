using NBitcoin;

namespace WalletWasabi.WabiSabiClientLibrary.Models.GetAnonymityScores;

public record AddressAnonymity(string address, double anonymitySet)
{
}
