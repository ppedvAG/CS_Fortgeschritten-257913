namespace Multitasking;

internal class _03_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = new Task<int>(Calculate);
		t.Start();

		bool hasPrinted = false;
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Console.WriteLine($"Main Thread: {i}");

			//Aufgabe: Mitten in der Schleife printen
			if (t.IsCompletedSuccessfully && !hasPrinted) //Sehr aufwändig
			{
				//Problem: Blockade des Main Threads
				//Lösung: ContinueWith
				Console.WriteLine(t.Result);
				hasPrinted = true;
			}
		}

		Console.ReadKey();
	}

	static int Calculate()
	{
		Thread.Sleep(500);
		return Random.Shared.Next();
	}
}
