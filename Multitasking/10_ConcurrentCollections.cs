using System.Collections.Concurrent;

namespace Multitasking;

internal class _10_ConcurrentCollections
{
	static void Main(string[] args)
	{
		ConcurrentBag<int> bag = [];
		bag.Add(1);
		bag.Add(2);
		bag.Add(3);
		//Kein Index, stattdessen TryPeek, TryTake

		SynchronizedCollection<int> list = [];
		list.Add(1);
		list.Add(2);
		list.Add(3);
		Console.WriteLine(list[0]);

		ConcurrentDictionary<int, string> dict = [];
	}
}
