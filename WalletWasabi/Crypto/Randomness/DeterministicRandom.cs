using WabiSabi.Crypto.Randomness;

namespace WalletWasabi.Crypto.Randomness;

public class DeterministicRandom : WasabiRandom
{
	// See https://lemire.me/blog/2019/03/19/the-fastest-conventional-random-number-generator-that-can-pass-big-crush/
	// See https://en.wikipedia.org/wiki/Lehmer_random_number_generator
	private readonly UInt128 _multiplier = 0xda942042e4dd58b5;
	private UInt128 _state = 0;

	public DeterministicRandom(int seed)
	{
		_state = (UInt128)seed + 1;
		Next();
	}

	private UInt128 Next()
	{
		_state *= _multiplier;
		return _state >> 64;
	}

	public override void GetBytes(byte[] buffer)
	{
		for (int i = 0; i < buffer.Length; i++)
		{
			buffer[i] = (byte)GetInt(0, byte.MaxValue + 1);
		}
	}

	public override void GetBytes(Span<byte> buffer)
	{
		for (int i = 0; i < buffer.Length; i++)
		{
			buffer[i] = (byte)GetInt(0, byte.MaxValue + 1);
		}
	}

	public override int GetInt(int fromInclusive, int toExclusive) => (int)GetInt64(fromInclusive, toExclusive);

	public long GetInt64(long fromInclusive, long toExclusive) => (long)(Next() % ((UInt128)toExclusive - (UInt128)fromInclusive) + (UInt128)fromInclusive);
}
