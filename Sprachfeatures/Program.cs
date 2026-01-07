namespace Sprachfeatures;

using DictId = Dictionary<int, Person>;

internal class Program
{
	private static void Main(string[] args)
	{
		//out
		//out: Mehrere Rückgabewerte ermöglichen
		//int r;
		bool b = int.TryParse("123", out int res); //Identitsch zu separater Variable
		Console.WriteLine(res);

		object o = 1;
		//if (o is int)
		//{
		//	int x = (int)o;
		//}

		if (o is int x) //Automatischer Cast
		{

		}

		//Tupel
		string[] strings = ["Hallo", "Welt"];
		Console.WriteLine(strings[0] + " " + strings[1]);

		(string h, string w) str = ("Hallo", "Welt");
		Console.WriteLine(str.h + " " + str.w);

		double zahl = 1_348_315_913.1_3_5_9_8_2_7_1_2_3_5;
		Console.WriteLine(zahl);

		//ref
		int z = 5;
		int z2 = z;
		z = 10;

		int r = 5;
		ref int r2 = ref r;
		r = 10;

		TestClass c = new TestClass();
		c.Zahl = 5;
		TestClass c2 = c;
		c.Zahl = 10;

		//Switch Pattern
		int temp = 5;
		//string wetter;
		//switch (temp)
		//{
		//	case < 0: //temp < 0
		//		wetter = "Kalt";
		//		break;
		//	case > 0 and < 10:
		//		wetter = "Frisch";
		//		break;
		//	case > 10 and < 20:
		//		wetter = "Warm";
		//		break;
		//	case > 20:
		//		wetter = "Heiß";
		//		break;
		//}

		string wetter = temp switch
		{
			< 0 => "Kalt",
			> 0 and < 10 => "Frisch",
			> 10 and < 20 => "Warm",
			> 20 => "Heiß",
			_ => "Andere"
		};

		//using
		//Schließt einen Stream automatisch
		//Sollte immer bei allen Klassen verwendet werden, die auf externe Ressourcen zugreifen
		//z.B.: StreamWriter, StreamReader, FileStream, DbConnection, HttpClient, ...
		//-> Alle Klassen, die das IDisposable Interface implementieren
		using StreamWriter sw = new StreamWriter("File.txt");
		//sw.Close();

		//static int Add()
		//{
		//	return z + r; //Kein Zugriff auf Variablen in der umliegenden Methode
		//}

		#region Strings
		//String-Interpolation ($-String): Code in einen String einbauen
		string ausgabe = "Zahl r: " + r + ", Zahl z: " + z;
		Console.WriteLine(ausgabe);

		string interpolation = $"Zahl r: {r}, Zahl z: {z}";
		Console.WriteLine(interpolation);

		Console.WriteLine($"Zahl größer 10: {(z > 10 ? "Ja": "Nein")}");
		//Console.WriteLine($"TestClass: {(c ?? "null")}");
		Console.WriteLine($"Wetter: {(temp switch
		{
			< 0 => "Kalt",
			> 0 and < 10 => "Frisch",
			> 10 and < 20 => "Warm",
			> 20 => "Heiß",
			_ => "Andere"
		})}");
		Console.WriteLine($"Ausgabe: {HalloWelt()}");

		//Verbatim-String (@-String): String, welcher Escape Sequenzen ignoriert
		string verbatim = @"\n\r\\";
		Console.WriteLine(verbatim);

		string pfad = @"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\9.0.10\System.Threading.dll";
		Console.WriteLine(pfad);
		#endregion

		IEnumerable<int> e;
		e = new int[10];
		e = new List<int>();

		//Dictionary<int, Person> p;
		//DictId p;

		Point p = new Point(1, 2);

		int a = 5;
		a.PrintNumber();
		temp.PrintNumber();

		double d = 123.456;
		int w = (int) d;

		int k = 123;
		double m = k;
	}

	public void PrintHalloWelt() => Console.WriteLine("Hallo Welt");

	public static string HalloWelt() => "Hallo Welt!";
}

public partial class TestClass
{
	protected internal int Zahl;

	public string Name { get; set; }

	//public string get_Name() { }

	//public void set_Name(string str) { }

	//private string name;

	public TestClass()
	{
		Zahl = 5;
	}
}

public record Person(int ID, string Vorname, string Nachname, int Alter)
{
	public void Test()
	{

	}
}

public class Point(int x, int y)
{
	public int X { get; set; } = x;

	public int Y { get; set; } = y;

	public static bool operator ==(Point a, Point b)
	{
		return a.X == b.X && a.Y == b.Y;
	}

	public static bool operator !=(Point a, Point b)
	{
		return !(a == b);
	}
}

public static class ExtensionMethods
{
	public static void PrintNumber(this int x)
	{
		Console.WriteLine(x);
	}
}