using WalletWasabi.WabiSabiClientLibrary.Models.AnalyzeTransactions;

namespace WalletWasabi.WabiSabiClientLibrary.Models;

public record AnalyzeTransactionsRequest(
	InternalInput[] InternalInputs,
	InternalOutput[] InternalOutputs,
	ExternalInput[] ExternalInputs,
	ExternalOutput[] ExternalOutputs
);
