using System.Collections;

internal class Program
{
	private static void Main(string[] args)
	{
		//Generics
		//Platzhalter für konkrete Typen bei Erstellung des Objektes
		List<int> ints = [];
		ints.Add(1); //T wird hier durch int ersetzt

		List<string> str = [];
		str.Add("Hallo"); //T wird hier durch string ersetzt

		////////////////////////////////////////////////////////////

		DataStore<int> ds = [];
		ds.Add(1);
		ds.Add(2);
		ds.Add(3);

		Console.WriteLine(ds[2]);

		foreach (int x in ds)
		{
			Console.WriteLine(x);
		}

		int z = Convert<int>("a");
	}

	public static T Convert<T>(object o)
	{
		//Keywords
		Console.WriteLine(typeof(T)); //Gibt den Typen hinter T zurück (Type Objekt)
		Console.WriteLine(nameof(T)); //Gibt eine Stringrepräsentation von T zurück ("Int32", "String", ...)
		Console.WriteLine(default(T)); //Gibt den Standardwert von T zurück

		//T obj = null; //Nur mit Constraints möglich

		return (T) o;
	}
}

public class DataStore<T> : IEnumerable<T>
{
	private List<T> _items { get; } = [];

	public void Add(T item)
	{
		_items.Add(item);
	}

	public T this[int index]
	{
		get => _items[index];
	}

	public IEnumerator<T> GetEnumerator()
	{
		return _items.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}