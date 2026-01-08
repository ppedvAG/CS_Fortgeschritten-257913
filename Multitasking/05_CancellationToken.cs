namespace Multitasking;

internal class _05_CancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new CancellationTokenSource(); //Am Anfang muss eine Source erstellt werden, diese stellt die Tokens aus
		CancellationToken token = cts.Token; //Wenn hier ein Token entnommen wird, wird eine Kopie erzeugt (weil struct)

		Task t = new Task(Run, token);
		t.Start();

		cts.CancelAfter(500);

		Console.ReadKey();
	}

	static void Run(object o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				//Option 1
				if (ct.IsCancellationRequested)
				{
					Console.WriteLine("Task beendet");
					break;
				}

				//Option 2
				ct.ThrowIfCancellationRequested(); //WICHTIG: Wenn ein Task abstürzt, läuft das Programm normal weiter

				Thread.Sleep(25);
				Console.WriteLine($"Task: {i}");
			}
		}
	}
}
