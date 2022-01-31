using NBitcoin;
using NBitcoin.Protocol;

namespace WalletWasabi.Helpers;

public static class Constants
{
	public const string ClientSupportBackendVersionMin = "4";
	public const string ClientSupportBackendVersionMax = "4";
	public const string BackendMajorVersion = "4";

	/// <summary>
	/// By changing this, we can force to start over the transactions file, so old incorrect transactions would be cleared.
	/// It is also important to force the KeyManagers to be reindexed when this is changed by renaming the BlockState Height related property.
	/// </summary>
	public const string ConfirmedTransactionsVersion = "2";

	public const uint ProtocolVersionWitnessVersion = 70012;

	public const int NonSegwitByteInWeightUnits = 4;
	public const int SegwitByteInWeightUnits = 1;
	public const int VirtualByteInWeightUnits = 4;
	public static int WeightUnitsToVirtualSize(int VirtualSize) => VirtualSize / VirtualByteInWeightUnits + (VirtualSize % VirtualByteInWeightUnits == 0 ? 0 : 1); // ceiling(VirtualSize / VirtualByteInWeightUnits)

	// txid (32) + vout (4) + scriptSig length (1 if the length is at most 252) + sequence (4)
	public const int InputBaseSizeInBytes = 41;

	// value (8) + scriptPubKey size (1 if the length is at most 252)
	public const int OutputBaseSizeInBytes = 9;

	public const int P2wpkhScriptSigSizeInBytes = 0;
	// version (1) + OP_PUSHDATA (1) + public key hash (20)
	public const int P2wpkhScriptPubkeySizeInBytes = 22;
	// OP_PUSHDATA (1) + signature (at most 73) + OP_PUSHDATA (1) + compressed public key (33)
	public const int P2wpkhWitnessMaximumSizeInBytes = 108;
	public const int P2wpkhInputMaximumSizeInWeightUnits = SegwitByteInWeightUnits * P2wpkhWitnessMaximumSizeInBytes + NonSegwitByteInWeightUnits * (InputBaseSizeInBytes + P2wpkhScriptSigSizeInBytes); // 272
	public const int P2wpkhOutputSizeInWeightUnits = NonSegwitByteInWeightUnits * (OutputBaseSizeInBytes + P2wpkhScriptPubkeySizeInBytes); // 124
	public static readonly int P2wpkhInputMaximumVirtualSize = WeightUnitsToVirtualSize(P2wpkhInputMaximumSizeInWeightUnits); // 68
	public static readonly int P2wpkhOutputVirtualSize = WeightUnitsToVirtualSize(P2wpkhOutputSizeInWeightUnits); // 31

	// OP_DUP (1) + OP_HASH160 (1) + OP_PUSHDATA (1) + pubkey_hash (20) + OP_EQUALVERIFY (1) + OP_CHECKSIG (1)
	public const int P2pkhScriptPubkeySizeInBytes = 25;
	public const int P2pkhOutputSizeInWeightUnits = NonSegwitByteInWeightUnits * (OutputBaseSizeInBytes + P2pkhScriptPubkeySizeInBytes); // 136
	public static readonly int P2pkhOutputVirtualSize = WeightUnitsToVirtualSize(P2pkhOutputSizeInWeightUnits); // 34

	// https://en.bitcoin.it/wiki/Bitcoin
	// There are a maximum of 2,099,999,997,690,000 Bitcoin elements (called satoshis), which are currently most commonly measured in units of 100,000,000 known as BTC. Stated another way, no more than 21 million BTC can ever be created.
	public const long MaximumNumberOfSatoshis = 2099999997690000;

	public const decimal MaximumNumberOfBitcoins = 20999999.9769m;

	public const int FastestConfirmationTarget = 1;
	public const int TwentyMinutesConfirmationTarget = 2;
	public const int OneDayConfirmationTarget = 144;
	public const int SevenDaysConfirmationTarget = 1008;

	public const int BigFileReadWriteBufferSize = 1 * 1024 * 1024;

	public const int DefaultMainNetBitcoinP2pPort = 8333;
	public const int DefaultTestNetBitcoinP2pPort = 18333;
	public const int DefaultRegTestBitcoinP2pPort = 18444;

	public const int DefaultMainNetBitcoinCoreRpcPort = 8332;
	public const int DefaultTestNetBitcoinCoreRpcPort = 18332;
	public const int DefaultRegTestBitcoinCoreRpcPort = 18443;

	public const decimal DefaultDustThreshold = 0.00005m;

	public const string AlphaNumericCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
	public const string CapitalAlphaNumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

	public const string ExecutableName = "wassabee";
	public const string AppName = "Wasabi Wallet";
	public const string BuiltinBitcoinNodeName = "Bitcoin Knots";

	public static readonly Version ClientVersion = new(1, 1, 13, 0);
	public static readonly Version HwiVersion = new("2.0.2");
	public static readonly Version BitcoinCoreVersion = new("21.2");
	public static readonly Version LegalDocumentsVersion = new(2, 0);

	public static readonly NodeRequirement NodeRequirements = new()
	{
		RequiredServices = NodeServices.NODE_WITNESS,
		MinVersion = ProtocolVersionWitnessVersion,
		MinProtocolCapabilities = new ProtocolCapabilities { SupportGetBlock = true, SupportWitness = true, SupportMempoolQuery = true }
	};

	public static readonly NodeRequirement LocalNodeRequirements = new()
	{
		RequiredServices = NodeServices.NODE_WITNESS,
		MinVersion = ProtocolVersionWitnessVersion,
		MinProtocolCapabilities = new ProtocolCapabilities { SupportGetBlock = true, SupportWitness = true }
	};

	public static readonly ExtPubKey FallBackCoordinatorExtPubKey = NBitcoinHelpers.BetterParseExtPubKey("xpub6BgAZqHhxw6pgEi2F38w5RBqctqCEoVWqcMdrn1epQZceKHtn8f8zHBduM3fwYQEKEGUf4efD6qRPc9wvDF4neoc6JjDbHNiaHbs3we5qL3");
	public static readonly ExtPubKey WabiSabiFallBackCoordinatorExtPubKey = NBitcoinHelpers.BetterParseExtPubKey("xpub6BgAZqHhxw6pgEi2F38w5RBqctqCEoVWqcMdrn1epQZceKHtn8f8zHBduM3fwYQEKEGUf4efD6qRPc9wvDF4neoc6JjDbHNiaHbs3we5qL3");

	public static readonly string[] UserAgents = new[]
	{
			"/Satoshi:0.21.0/",
			"/Satoshi:0.20.1/",
			"/Satoshi:0.20.0/",
			"/Satoshi:0.19.1/",
			"/Satoshi:0.19.0.1/",
			"/Satoshi:0.19.0/",
			"/Satoshi:0.18.1/",
			"/Satoshi:0.18.0/",
			"/Satoshi:0.17.1/",
			"/Satoshi:0.17.0.1/",
			"/Satoshi:0.17.0/",
			"/Satoshi:0.16.3/",
			"/Satoshi:0.16.2/",
			"/Satoshi:0.16.1/",
			"/Satoshi:0.16.0/",
		};

	public static readonly int[] ConfirmationTargets = new[]
	{
			2, // Twenty Minutes
			3, // Thirty Minutes
			6, // One Hour
			18, // Three Hours
			36, // Six Hours
			72, // Twelve Hours
			144, // One Day
			432, // Three Days
			1008, // Seven Days
		};

	public static string ClientSupportBackendVersionText => ClientSupportBackendVersionMin == ClientSupportBackendVersionMax
			? ClientSupportBackendVersionMin
			: $"{ClientSupportBackendVersionMin} - {ClientSupportBackendVersionMax}";
}
