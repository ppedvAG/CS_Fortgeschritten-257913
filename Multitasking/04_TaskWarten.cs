namespace Multitasking;

internal class _04_TaskWarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		t.Wait(); //Stoppt den Main Thread, bis der Task fertig ist

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");

		Task t2 = null;
		Task t3 = null;
		Task.WaitAll(t, t2, t3);
		Task.WaitAny(t, t2, t3); //Rückgabewert zeigt den spezifischen Task an

		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Task: {i}");
	}
}
