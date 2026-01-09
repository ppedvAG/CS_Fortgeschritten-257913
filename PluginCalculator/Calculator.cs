using PluginBase;

namespace PluginCalculator;

/// <summary>
/// Hier muss eine Abhängigkeit zum PluginBase Projekt hergestellt werden
/// </summary>
public class Calculator : IPlugin
{
	public string Name => "Rechner";

	public string Description => "Ein einfacher Rechner";

	public string Version => "1.0";

	public string Author => "Lukas Kern";

	[ReflectionVisible]
	public double Addiere(double x, double y)
	{
		return x + y;
	}

	[ReflectionVisible]
	public double Subtrahiere(double x, double y)
	{
		return x - y;
	}
}
