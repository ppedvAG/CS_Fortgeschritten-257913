namespace Events;

/// <summary>
/// Warum Events?
/// 
/// User freie Wahl geben, was bei den Methoden passieren soll
/// z.B.: Konsolenoutput, Log, Datenbank, GUI, ...
/// </summary>
internal class User
{
	static void Main(string[] args)
	{
		Component comp = new Component();
		comp.Start += Comp_Start;
		comp.End += Comp_End;
		comp.Progress += Comp_Progress;
		comp.Run();
	}

	private static void Comp_Start()
	{
		Console.WriteLine($"[{DateTime.Now}]: Prozess gestartet");
	}

	private static void Comp_End()
	{
		Console.WriteLine($"[{DateTime.Now}]: Prozess fertig");
	}

	private static void Comp_Progress(int obj)
	{
		Console.WriteLine($"[{DateTime.Now}]: Fortschritt: {obj}");
	}
}