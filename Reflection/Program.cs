using System.Reflection;

namespace Reflection;

internal class Program
{
	public string Text { get; set; } = "Hallo Welt";

	public static int Counter { get; set; } = 0;

	private string Test;

	static void Main(string[] args)
	{
		//Type Objekt
		//Enthält alle möglichen Informationen zu einem Objekt

		//2 Optionen
		Type pt = typeof(Program); //Über einen Typnamen

		Program p = new Program();
		Type t = p.GetType(); //Über ein Objekt

		////////////////////////////////////////////////////

		//Arbeiten mit dem Type Objekt
		Console.WriteLine(t.GetProperty("Text").GetValue(p));
		t.GetProperty("Text").SetValue(p, "Bye Welt");

		Console.WriteLine(p.Text);

		t.GetMethod("Hallo").Invoke(p, null);

		//Static
		t.GetProperty("Counter").SetValue(null, 1); //Bei Static Membern muss der obj-Parameter null sein

		//Private mit Reflection umgehen
		t.GetField("Test", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(p, "Keine Ahnung");

		////////////////////////////////////////////////////

		//Activator
		//Aus einem Type Objekt ein normales C# Objekt erstellen
		//Wird vorallem bei externen DLLs verwendet
		object o = Activator.CreateInstance(t);

		//Assembly
		//Sammlung von Typen (DLL mit Inhalt)
		Assembly a = Assembly.GetExecutingAssembly(); //Derzeitiges Projekt

		Type[] types = a.GetTypes(); //Alle Typen aus dem Projekt anschauen

		////////////////////////////////////////////////////

		//Aufgabe: Component aus Delegates & Events dynamisch laden und verwenden
		Assembly loaded = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2026_01_07\Events\bin\Debug\net9.0\Events.dll");

		Type compType = loaded.GetType("Events.Component");

		object comp = Activator.CreateInstance(compType);

		compType.GetEvent("Start").AddEventHandler(comp, () => Console.WriteLine("Reflection Start"));
		compType.GetEvent("End").AddEventHandler(comp, () => Console.WriteLine("Reflection End"));
		compType.GetEvent("Progress").AddEventHandler(comp, (int x) => Console.WriteLine($"Reflection Progress: {x}"));

		compType.GetMethod("Run").Invoke(comp, null);
	}

	public void Hallo()
	{
		Console.WriteLine("Hallo Welt!");
	}
}
