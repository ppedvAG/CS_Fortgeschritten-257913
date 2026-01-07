namespace Delegates;

internal class ActionFunc
{
	static void Main(string[] args)
	{
		//Action und Func
		//Vorgegebene Delegates in C#, die an vielen Stellen verwendet werden
		//z.B.: TPL, Linq, Reflection, ...
		//Essentiell für die fortgeschrittene Programmierung

		//Action: Delegate, welches void zurückgibt, und bis zu 16 Parameter hat
		Action<int, int> a = Addieren;
		a(2, 3);
		a?.Invoke(2, 3);

		List<int> zahlen = [1, 2, 3, 4, 5];
		zahlen.ForEach(Print);

		/////////////////////////////////////////////////////

		//Func: Delegate, welches einen beliebigen Wert zurückgibt, und bis zu 16 Parameter hat
		Func<int, int, double> f = Dividieren;
		double d = f(4, 5);
		//double d2 = f?.Invoke(4, 5); //Bei ?.Invoke kann null zurückkommen, wenn die Func selbst null ist

		double? d3 = f?.Invoke(4, 5);
		double d4 = f?.Invoke(4, 5) ?? double.NaN;

		zahlen.Where(TeilbarDurch2); //Finde alle Zahlen, die durch 2 teilbar sind

		/////////////////////////////////////////////////////

		//Anonyme Funktionen: Methodenzeiger, welche nicht dediziert erstellt werden, sondern nur bei dem Action-/Funcparameter eingesetzt werden
		//-> werden einmal verwendet und weggeworfen

		Func<int, int, double> div;
		div = delegate (int x, int y)
		{
			return (double) x / y;
		};

		div += (int x, int y) =>
		{
			return (double) x / y;
		};

		div += (int x, int y) => (double) x / y;

		div += (x, y) => (double) x / y;

		//Funktionen mit Delegate Parametern mit anonymen Funktionen befüllen
		zahlen.ForEach(x => Console.WriteLine(x));
		zahlen.ForEach(Console.WriteLine);

		zahlen.Where(x => x % 2 == 0);

		string text = "Hallo Welt";
		bool b = text.All(char.IsLetter); //Sind alle Zeichen Buchstaben?
	}

	#region Action
	static void Addieren(int x, int y)
	{
		Console.WriteLine($"{x} + {y} = {x + y}");
	}

	static void Print(int x)
	{
		Console.WriteLine($"Zahl: {x}");
	}
	#endregion

	#region Func
	static double Dividieren(int x, int y)
	{
		return (double) x / y;
	}

	static bool TeilbarDurch2(int x)
	{
		return x % 2 == 0;
	}
	#endregion
}
