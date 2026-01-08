namespace Multitasking;

internal class _07_ContinueWith
{
	static void Main(string[] args)
	{
		//Task<int> t = new Task<int>(Calc);
		//t.ContinueWith(Folgetask); //Bei ContinueWith gibt es immer Zugriff auf den vorherigen Task
		//t.ContinueWith(v => Console.WriteLine($"Ergebis: {v.Result}")); //Anonym
		//t.Start(); //ACHTUNG: Bei Task.Run/Task.Factory.StartNew kann es passieren, das der Task fertig ist, bevor der Folgetask (ContinueWith) angehängt wird

		//Console.ReadKey();

		////////////////////////////////////////////////////////////

		//Folgetasks mit Bedingungen
		Task<int> t = new Task<int>(Calc);
		t.ContinueWith(Folgetask); //Dieser Task wird immer ausgeführt (keine Bedingung)
		t.ContinueWith(v => Console.WriteLine($"Ergebis: {v.Result}"), TaskContinuationOptions.OnlyOnRanToCompletion);
		t.ContinueWith(v => Console.WriteLine(v.Exception.Message), TaskContinuationOptions.OnlyOnFaulted);
		t.Start();

		Console.ReadKey();
	}

	static int Calc()
	{
		Thread.Sleep(500);
		if (Random.Shared.Next() % 2 == 0)
			throw new Exception("50%");
		return Random.Shared.Next();
	}

	static void Folgetask(Task<int> t)
	{
		Console.WriteLine($"Ergebis: {t.Result}");
	}
}
