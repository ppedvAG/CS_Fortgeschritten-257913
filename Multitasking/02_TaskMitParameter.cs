namespace Multitasking;

internal class _02_TaskMitParameter
{
	static void Main(string[] args)
	{
		Task t = new Task(Run, 200);
		t.Start();

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");

		Console.ReadKey();
	}

	static void Run(object o)
	{
		if (o is int x)
		{
			for (int i = 0; i < x; i++)
			{
				Console.WriteLine($"Task: {i}");
			}
		}
	}
}