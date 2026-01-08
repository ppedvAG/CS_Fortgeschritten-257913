namespace Multitasking;

public class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start(); //WICHTIG: Start nicht vergessen

		Task t2 = Task.Factory.StartNew(Run); //ab .NET 4.0

		Task t3 = Task.Run(Run); //ab .NET 4.5

		//Ab hier parallel

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");

		Console.ReadKey(); //WICHTIG: Wenn der Main Thread zu Ende ist, werden die Tasks abgebrochen
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Task: {i}");
	}
}
