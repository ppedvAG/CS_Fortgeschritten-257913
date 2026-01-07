namespace Delegates;

internal class Delegates
{
	public delegate void Vorstellung(string name); //Definition eines eigenen Delegates

	private static void Main(string[] args)
	{
		Vorstellung v = new Vorstellung(VorstellungDE); //Erstellung des Delegates mit einem Methodenzeiger
		v("Max"); //Alle Methoden, die an dem Delegate hängen, werden hier ausgeführt

		//Delegate erweitern
		v += new Vorstellung(VorstellungEN);
		v += VorstellungEN; //Kurzform
		v("Udo");

		//Delegate reduzieren
		v -= VorstellungDE;
		v -= VorstellungDE; //Wenn die Methode nicht mehr angehängt ist, gibt es hier keine Fehlermeldung
		v -= VorstellungDE;
		v -= VorstellungDE;
		v("Tim");

		v -= VorstellungEN;
		v -= VorstellungEN;
		//v("Max"); //Das Delegate ist hier null (kein Delegate Objekt mehr vorhanden)

		//WICHTIG: Bei Delegates sollten immer Null Checks gemacht werden
		if (v != null)
			v("Max");

		v?.Invoke("Max"); //Selbiges wie oben, aber mit null propagation

		foreach (Delegate dg in v.GetInvocationList())
		{
			Console.WriteLine(dg.Method.Name); //Alle Methoden iterieren
		}
	}

	static void VorstellungDE(string name)
	{
		Console.WriteLine($"Hallo mein Name ist {name}");
	}

	static void VorstellungEN(string name)
	{
		Console.WriteLine($"Hello my name is {name}");
	}
}