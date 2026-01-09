using PluginBase;
using System.Reflection;

namespace PluginClient;

/// <summary>
/// Hier muss eine Abhängigkeit zum PluginBase Projekt hergestellt werden
/// </summary>
internal class Program
{
	static void Main(string[] args)
	{
		string calcPath = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2026_01_07\PluginCalculator\bin\Debug\net9.0\PluginCalculator.dll";
		IPlugin calcPlugin = LoadPlugin(calcPath);

		////////////////////////////////////////////

		Type ct = calcPlugin.GetType();
		foreach (PropertyInfo pi in ct.GetProperties())
		{
			Console.WriteLine($"{pi.Name}: {pi.GetValue(calcPlugin)}");
		}

		MethodInfo[] array = ct.GetMethods().Where(e => e.GetCustomAttribute<ReflectionVisibleAttribute>() != null).ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			Console.WriteLine($"{i}: {array[i].Name}");
		}
	}

	static IPlugin LoadPlugin(string path)
	{
		Assembly a = Assembly.LoadFrom(path);
		if (a == null)
			throw new DllNotFoundException($"{path} konnte nicht gefunden werden");

		Type? foundType = a.GetTypes().FirstOrDefault(e => e.GetInterface(nameof(IPlugin)) != null);
		if (foundType == null)
			throw new TypeLoadException($"Innerhalb der DLL konnte kein Plugin gefunden werden");

		return (IPlugin) Activator.CreateInstance(foundType);
	}
}
