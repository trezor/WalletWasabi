using NBitcoin;

namespace WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

public record Tx(
	InternalInput[] InternalInputs,
	InternalOutput[] InternalOutputs,
	ExternalInput[] ExternalInputs,
	ExternalOutput[] ExternalOutputs
);
