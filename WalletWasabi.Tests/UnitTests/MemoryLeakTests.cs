using Xunit;
using WalletWasabi.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WalletWasabi.Tests.UnitTests;

public class MemoryLeakTests
{
	[Fact]
	public void CombinationsWithoutRepetitionRecursiveMemoryLeakTest()
	{
		TestCombinationsWithoutRepetition(LinqExtensions.CombinationsWithoutRepetition);
	}

	[Fact]
	public void CombinationsWithoutRepetitionIterativeMemoryLeakTest()
	{
		TestCombinationsWithoutRepetition(LinqExtensions.CombinationsWithoutRepetitionIterative);
	}

	private void TestCombinationsWithoutRepetition(CombinationsWithoutRepetitionDelegate<int> function)
	{
		var memoryBefore = GetMemoryInMiB();
		var combinations = function(Enumerable.Range(0, 20), 10).ToArray();
		Assert.Equal(combinations.Count(), 184756); // Sanity check
		var memoryAfter = GetMemoryInMiB();
		if (memoryAfter > memoryBefore * 1.1)
		{
			Console.WriteLine($"Memory before: {memoryBefore} MiB");
			Console.WriteLine($"Memory after: {memoryAfter} MiB");
			Console.WriteLine($"Memory difference: {memoryAfter - memoryBefore} MiB");
			Assert.True(false);
		}
	}

	private static float GetMemoryInMiB()
	{
		return ((float)GetMemoryInB()) / 1024 / 1024;
	}

	private static long GetMemoryInB()
	{
		GC.Collect();
		GC.WaitForPendingFinalizers();
		GC.Collect();
		return Process.GetCurrentProcess().PrivateMemorySize64;
	}

	private delegate IEnumerable<IEnumerable<T>> CombinationsWithoutRepetitionDelegate<T>(IEnumerable<T> input, int ofLength);
}
