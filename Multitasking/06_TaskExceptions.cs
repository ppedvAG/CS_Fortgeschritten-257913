namespace Multitasking;

internal class _06_TaskExceptions
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		try
		{
			t.Wait();
		}
		catch (AggregateException ex)
		{
			Console.WriteLine(ex.InnerException.Message);
			throw; //Programm wirklich zum Absturz bringen
		}

		Console.ReadKey();
	}

	static void Run()
	{
		Thread.Sleep(500);
		throw new Exception("Hallo");
	}
}
