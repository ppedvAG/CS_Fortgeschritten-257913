using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Reflection;

public static class CustomXmlSerializer
{
	static void Main(string[] args)
	{
		Fahrzeug fzg = new Fahrzeug(251, FahrzeugMarke.BMW);
		Console.WriteLine(Serialize(fzg));
	}

	public static string Serialize<T>(T o)
	{
		Type t = o.GetType();
		PropertyInfo[] props = t.GetProperties();
		StringBuilder sb = new();

		sb.Append("<");
		sb.Append(t.Name);
		sb.Append(">\n");

		foreach (PropertyInfo pi in props)
		{
			sb.Append("\t<");
			sb.Append(pi.Name);
			sb.Append(">");
			sb.Append(pi.GetValue(o));
			sb.Append("</");
			sb.Append(pi.Name);
			sb.Append(">\n");
		}

		sb.Append("</");
		sb.Append(t.Name);
		sb.Append(">\n");

		return sb.ToString();
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