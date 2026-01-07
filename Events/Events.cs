namespace Events;

internal class Events
{
	private static void Main(string[] args) => new Events().Run();

	/////////////////////////////////////////////////////////

	public event EventHandler TestEvent; //Anlegen des Events (Entwicklerseite)

	public event EventHandler<CustomEventArgs> ArgsEvent;

	public event EventHandler<int> CounterEvent;

	public void Run()
	{
		TestEvent += Events_TestEvent; //Anhängen der Methode (Anwenderseite)

		TestEvent?.Invoke(this, EventArgs.Empty); //Ausführen des Events (Entwicklerseite)

		////////////////////////////////////////////////////////

		ArgsEvent += Events_ArgsEvent;

		ArgsEvent?.Invoke(this, new CustomEventArgs() { Message = "Hello" });

		////////////////////////////////////////////////////////

		CounterEvent += Events_CounterEvent;

		CounterEvent?.Invoke(this, 10);
	}

	private void Events_TestEvent(object? sender, EventArgs e)
	{
		Console.WriteLine("TestEvent ausgeführt");
	}

	private void Events_ArgsEvent(object? sender, CustomEventArgs e)
	{
		Console.WriteLine($"Nachricht: {e.Message}");
	}

	private void Events_CounterEvent(object? sender, int e)
	{
		Console.WriteLine($"Zähler: {e}");
	}
}

public class CustomEventArgs : EventArgs
{
	public string Message { get; set; }
}