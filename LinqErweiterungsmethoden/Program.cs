using System.Diagnostics;
using System.Xml.Serialization;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		//IEnumerable
		//Anleitung zur Erstellung von Daten
		IEnumerable<int> zahlen = Enumerable.Range(0, 1_000_000_000); //1ms

		//Wenn ein IEnumerable iteriert wird (foreach, ToList, ToArray, ...), werden die Daten erzeugt
		//List<int> zahlenList = Enumerable.Range(0, 1_000_000_000).ToList(); //1s, 4GB an Daten

		//Wenn hier zahlenList übergeben wird, wird die Liste zweimal iteriert (einmal oben bei ToList() und hier bei AddRange())
		//Wenn hier zahlen übergeben wird, wird die Anleitung nur einmal iteriert (hier bei AddRange())
		List<int> z = [];
		z.AddRange();

		///////////////////////////////////////////////////////

		//foreach funktioniert nur wegen GetEnumerator() (greift auf den Enumerator zu, und verwendet Current und MoveNext() um die Schleife durchzugehen)
		List<int> ints = [1, 2, 3, 4, 5];
		foreach (int x in ints) //x = Current
		{
			Console.WriteLine(x);
		} //MoveNext()
		  //Reset()


		//Schleife ohne Schleife
		IEnumerator<int> enumerator = ints.GetEnumerator();
		enumerator.MoveNext(); //Enumerator auf das erste Element bewegen
	start:
		int a = enumerator.Current;
		Console.WriteLine(a);
		if (enumerator.MoveNext())
			goto start;
		enumerator.Reset();

		///////////////////////////////////////////////////////

		//Einfaches Linq
		List<int> nummern = Enumerable.Range(1, 20).ToList();

		Console.WriteLine(nummern.Average());
		Console.WriteLine(nummern.Min());
		Console.WriteLine(nummern.Max());
		Console.WriteLine(nummern.Sum());

		Console.WriteLine(nummern.First()); //Erstes Element, wenn die Liste leer ist bekommen wir eine Exception
		Console.WriteLine(nummern.Last());
		Console.WriteLine(nummern.FirstOrDefault()); //Erstes Element, wenn die Liste leer ist bekommen wir default(int) = 0
		Console.WriteLine(nummern.LastOrDefault());

		Console.WriteLine(nummern.First(e => e % 2 == 0)); //Bei diesen 4 Funktion kann auch eine Bedingung angegeben werden
		Console.WriteLine(nummern.First(TeilbarDurch2)); //Alternative ohne Lambda Expression

		bool TeilbarDurch2(int y)
		{
			return y % 2 == 0;
		}

		//Console.WriteLine(nummern.First(e => e % 50 == 0)); //Exception
		Console.WriteLine(nummern.FirstOrDefault(e => e % 50 == 0)); //0

		///////////////////////////////////////////////////////

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
	 		new Fahrzeug(251, FahrzeugMarke.BMW),
	 		new Fahrzeug(274, FahrzeugMarke.BMW),
	 		new Fahrzeug(146, FahrzeugMarke.BMW),
	 		new Fahrzeug(208, FahrzeugMarke.Audi),
	 		new Fahrzeug(189, FahrzeugMarke.Audi),
	 		new Fahrzeug(133, FahrzeugMarke.VW),
	 		new Fahrzeug(253, FahrzeugMarke.VW),
	 		new Fahrzeug(304, FahrzeugMarke.BMW),
	 		new Fahrzeug(151, FahrzeugMarke.VW),
	 		new Fahrzeug(250, FahrzeugMarke.VW),
	 		new Fahrzeug(217, FahrzeugMarke.Audi),
	 		new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		//Where
		//Alle VWs finden
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW);

		//Alle VWs finden, die mind. 200km/h fahren können
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW && e.MaxV >= 200);
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW).Where(e => e.MaxV >= 200);

		//OrderBy
		fahrzeuge.OrderBy(e => e.Marke);
		fahrzeuge.OrderByDescending(e => e.Marke);

		//Subsequente Sortierungen
		fahrzeuge.OrderBy(e => e.Marke).ThenBy(e => e.MaxV);
		fahrzeuge.OrderByDescending(e => e.Marke).ThenByDescending(e => e.MaxV);

		//Where + OrderBy
		fahrzeuge
			.Where(e => e.Marke == FahrzeugMarke.BMW)
			.OrderBy(e => e.MaxV);

		//All und Any

		//Können alle Fahrzeuge mind. 150km/h fahren?
		if (fahrzeuge.All(e => e.MaxV >= 150))
		{
			//...
		}

		//Gibt es mind. ein Fahrzeug das 150km/h fahren kann?
		if (fahrzeuge.Any(e => e.MaxV >= 150))
		{
			//...
		}

		//Enthält die Liste Elemente?
		fahrzeuge.Any();

		//Count
		//Wieviele BMWs haben wir?
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.BMW); //4
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).Count(); //Suboptimal
		fahrzeuge.Count(); //Nicht verwenden; stattdessen .Count Property verwenden

		//MinBy, MaxBy
		fahrzeuge.MinBy(e => e.MaxV); //Das langsamste Fahrzeug (Audi, 125km/h)
		fahrzeuge.Min(e => e.MaxV); //Die Geschwindigkeit des langsamstes Fahrzeuges (125)
		
		fahrzeuge.Average(e => e.MaxV); //208.416666666
		fahrzeuge.Sum(e => e.MaxV);

		fahrzeuge.Select(e => e.MaxV).Average(); //Suboptimal

		//Skip & Take
		fahrzeuge.Skip(3); //alle Elemente außer die ersten 3
		fahrzeuge.Skip(3).Take(3); //Elemente Index 3-5 (fahrzeuge[3..5])

		//Webshop
		int page = 0;
		fahrzeuge.Skip(page * 10).Take(10);

		fahrzeuge.Chunk(10);

		//Die 3 schnellsten Fahrzeuge
		fahrzeuge
			.OrderByDescending(e => e.MaxV)
			.Take(3);

		//SelectMany
		List<List<int>> twoD = [[1, 2, 3], [4, 5, 6], [7, 8, 9]]; //3x3 Matrix
		twoD.SelectMany(e => e); //Liste zu einer 1D Liste konvertieren

		//GroupBy
		//Anhand eines Schlüssels die Elemente in Gruppen aufteilen
		//Schlüssel auswählen -> Listen erzeugen mit dem Schlüssel -> Elemente in die entsprechenden Listen hineinlegen
		IEnumerable<IGrouping<FahrzeugMarke, Fahrzeug>> g = fahrzeuge.GroupBy(e => e.Marke); //Sehr unpraktischer Typ

		Dictionary<FahrzeugMarke, List<Fahrzeug>> dict = fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(key => key.Key, fzg => fzg.ToList()); //IGrouping ist eine Liste -> mit ToList() wird es zu einer normalen Liste

		fahrzeuge.GroupBy(e => e.Marke).ToLookup(e => e.Key); //Lookup: Read-Only Dictionary (perfomanter, aber nicht beschreibbar)

		//Select
		//Transformiert die Liste in eine neue Form

		//2 Anwendungsfälle

		//1. Fall (80%): Einzelnes Feld entnehmen
		fahrzeuge.Select(e => e.Marke); //Liste nur mit Marken

		fahrzeuge
			.Select(e => e.Marke)
			.Distinct(); //Jede Marke nur einmal (Welche Marken haben wir?)

		//2. Fall (20%): Transformation
		Enumerable.Range(0, 100).Select(e => (double) e); //Gesamte Liste casten

		//Liste mit 0.25er Schritten
		Enumerable.Range(0, 100).Select(e => e / 4.0);

		//String Liste parsen
		List<string> str = ["1", "2", "3"];
		str.Select(e => int.Parse(e));
		str.Select(int.Parse); //Kurzform

		//Bei vollen Pfaden nur den Dateinamen nehmen
		string[] files = Directory.GetFiles("C:\\Windows");
		files.Select(e => Path.GetFileNameWithoutExtension(e));
		files.Select(Path.GetFileNameWithoutExtension); //Kurzform

		//Einen String in seine Char-Codes umwandeln
		string text = "Hallo Welt";
		text.Select(e => (int) e);

		//Erweiterungsmethoden
		int zahl = 18431;
		Console.WriteLine(zahl.Quersumme());

		Erweiterungsmethoden.Quersumme(zahl); //Compiler generiert hier einen normalen Methodenaufruf

		//Alle VWs
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW);
		Enumerable.Where(fahrzeuge, e => e.Marke == FahrzeugMarke.VW);

		fahrzeuge.Shuffle();
	}
}

[DebuggerDisplay("Marke: {Marke}, MaxV: {MaxV}")]
public class Fahrzeug
{
	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}
}

public enum FahrzeugMarke
{
	Audi, BMW, VW
}