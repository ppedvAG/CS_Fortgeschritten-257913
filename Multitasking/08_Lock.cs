namespace Multitasking;

internal class _08_Lock
{
	public static int Counter { get; set; }

	public static object Lock { get; set; } = new();

	static void Main(string[] args)
	{
		//List<Task> tasks = [];
		//for (int i = 0; i < 100; i++)
		//	tasks.Add(Task.Run(Increment));

		List<Task> tasks = Enumerable.Range(0, 100).Select(i => Task.Run(Increment)).ToList();
		Console.ReadKey();
	}

	static void Increment()
	{
		//Problem: Tasks erhöhen den Zähler, aber andere Tasks haben noch den alten Zustand
		//Lösung: Locking
		for (int i = 0; i < 100; i++)
		{
			lock (Lock) //Lock Object hält den Status von dem Lock (welcher Task und ob gerade gesperrt wird)
			{
				Counter++;
				Console.WriteLine(Counter);
			}

			//Monitor: 1:1 wie Lock, kann aber mit Methoden gesteuert werden
			//Monitor.Enter(Lock);
			//Counter++;
			//Console.WriteLine(Counter);
			//Monitor.Exit(Lock);

			//Interlocked.Add(ref Counter, 1); //automatisch gelocktes ++
		}
	}
}
