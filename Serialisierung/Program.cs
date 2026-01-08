using System.Diagnostics;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

internal class Program
{
	private static void Main(string[] args)
	{
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		 {
	 		new PKW(251, FahrzeugMarke.BMW),
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

		string filePath = "Fahrzeuge.xml";

		string folderPath = "Fahrzeuge";
		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		filePath = Path.Combine(folderPath, filePath);

		//1. Schreiben/Lesen
		XmlSerializer xml = new(fahrzeuge.GetType());
		using (StreamWriter sw = new StreamWriter(filePath))
		{
			xml.Serialize(sw, fahrzeuge);
		}

		//using (StreamReader sr = new StreamReader(filePath))
		//{
		//	List<Fahrzeug> fzg = (List<Fahrzeug>) xml.Deserialize(sr);
		//}

		List<Fahrzeug> fzg = xml.Deserialize<List<Fahrzeug>>(filePath); //Nach Erweiterungsmethode

		//3. Attribute
		//XmlIgnore
		//XmlAttribute
		//XmlInclude: Vererbung

		//4. XML per Hand
		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);

		foreach (XmlNode node in doc.DocumentElement.ChildNodes)
		{
			//int v = int.Parse(node.Attributes["MaxV"].InnerText);

			int v = node.GetProperty<int>("MaxV", true); //Nach Erweiterungsmethode
			FahrzeugMarke m = Enum.Parse<FahrzeugMarke>(node.Attributes["Marke"].InnerText);

			Console.WriteLine($"MaxV: {v}, Marke: {m}");
		}
	}

	static void SystemJson()
	{
		////System.Text.Json
		//List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		//{
		//	new PKW(251, FahrzeugMarke.BMW),
		//	new Fahrzeug(274, FahrzeugMarke.BMW),
		//	new Fahrzeug(146, FahrzeugMarke.BMW),
		//	new Fahrzeug(208, FahrzeugMarke.Audi),
		//	new Fahrzeug(189, FahrzeugMarke.Audi),
		//	new Fahrzeug(133, FahrzeugMarke.VW),
		//	new Fahrzeug(253, FahrzeugMarke.VW),
		//	new Fahrzeug(304, FahrzeugMarke.BMW),
		//	new Fahrzeug(151, FahrzeugMarke.VW),
		//	new Fahrzeug(250, FahrzeugMarke.VW),
		//	new Fahrzeug(217, FahrzeugMarke.Audi),
		//	new Fahrzeug(125, FahrzeugMarke.Audi)
		//};

		//string filePath = "Fahrzeuge.json";

		//string folderPath = "Fahrzeuge";
		//if (!Directory.Exists(folderPath))
		//	Directory.CreateDirectory(folderPath);

		//filePath = Path.Combine(folderPath, filePath);

		////1. Schreiben/Lesen
		////string json = JsonSerializer.Serialize(fahrzeuge);
		////File.WriteAllText(filePath, json);

		////string readJson = File.ReadAllText(filePath);
		////Fahrzeug[] readFzg = JsonSerializer.Deserialize<Fahrzeug[]>(readJson);

		////2. Settings/Options
		//JsonSerializerOptions options = new();
		//options.WriteIndented = true;
		//options.Converters.Add(new JsonStringEnumConverter());

		////WICHTIG: Options beim Lesen/Schreiben mitgeben
		//string json = JsonSerializer.Serialize(fahrzeuge, options);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg = JsonSerializer.Deserialize<Fahrzeug[]>(readJson, options);

		////3. Attribute

		////4. Json per Hand
		//JsonDocument doc = JsonDocument.Parse(json);
		//foreach (JsonElement element in doc.RootElement.EnumerateArray()) //EnumerateArray: Konvertiert die Json-Elemente zu einem Array
		//{
		//	int v = element.GetProperty("MaxV").GetInt32();
		//	FahrzeugMarke m = Enum.Parse<FahrzeugMarke>(element.GetProperty("Marke").GetString());

		//	Console.WriteLine($"MaxV: {v}, Marke: {m}");
		//}
	}

	static void NewtonsoftJson()
	{
		//Newtonsoft.Json
		//List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		//{
		//	new PKW(251, FahrzeugMarke.BMW),
		//	new Fahrzeug(274, FahrzeugMarke.BMW),
		//	new Fahrzeug(146, FahrzeugMarke.BMW),
		//	new Fahrzeug(208, FahrzeugMarke.Audi),
		//	new Fahrzeug(189, FahrzeugMarke.Audi),
		//	new Fahrzeug(133, FahrzeugMarke.VW),
		//	new Fahrzeug(253, FahrzeugMarke.VW),
		//	new Fahrzeug(304, FahrzeugMarke.BMW),
		//	new Fahrzeug(151, FahrzeugMarke.VW),
		//	new Fahrzeug(250, FahrzeugMarke.VW),
		//	new Fahrzeug(217, FahrzeugMarke.Audi),
		//	new Fahrzeug(125, FahrzeugMarke.Audi)
		//};

		//string filePath = "Fahrzeuge.json";

		//string folderPath = "Fahrzeuge";
		//if (!Directory.Exists(folderPath))
		//	Directory.CreateDirectory(folderPath);

		//filePath = Path.Combine(folderPath, filePath);

		////1. Schreiben/Lesen
		////string json = JsonConvert.SerializeObject(fahrzeuge);
		////File.WriteAllText(filePath, json);

		////string readJson = File.ReadAllText(filePath);
		////Fahrzeug[] readFzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson);

		////2. Settings
		//JsonSerializerSettings settings = new();
		//settings.Formatting = Formatting.Indented;
		//settings.Converters.Add(new StringEnumConverter());
		//settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbung aktivieren

		////WICHTIG: Settings beim Lesen/Schreiben mitgeben
		//string json = JsonConvert.SerializeObject(fahrzeuge, settings);
		//File.WriteAllText(filePath, json);

		////3. Attribute
		////JsonDerivedType wird hier per Settings durchgeführt
		//string readJson = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson, settings);

		////4. Json per Hand
		//JToken doc = JToken.Parse(json);
		//foreach (JToken element in doc)
		//{
		//	int v = element["MaxV"].Value<int>();
		//	string m = element["Marke"].Value<string>();

		//	Console.WriteLine($"MaxV: {v}, Marke: {m}");
		//}
	}
}

[DebuggerDisplay("Marke: {Marke}, MaxV: {MaxV}")]

//JsonDerivedType: Aktiviert Vererbung beim Schreiben/Lesen
//Muss auf die Klasse angewandt werden
//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(PKW), "P")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]
public class Fahrzeug
{
	//[JsonProperty(PropertyName = "Maximalgeschwindigkeit", Order = 1)]
	[XmlAttribute]
	public int MaxV { get; set; }

	[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}

	public Fahrzeug()
	{
		
	}

	//[Newtonsoft.Json.JsonExtensionData]
	//public Dictionary<string, object> Dict { get; set; }
}

public class PKW : Fahrzeug
{
	public PKW(int maxV, FahrzeugMarke marke) : base(maxV, marke) { }

	public PKW() { }
}

public enum FahrzeugMarke
{
	Audi, BMW, VW
}

public static class JsonExtensions
{
	public static T GetProperty<T>(this JsonElement e, string propName)
	{
		JsonElement element = e.GetProperty(propName);
		object o = default(T) switch //default(T): Generiert den Standardwert von T + den Typen von T
		{
			bool => element.GetBoolean(),
			byte[] => element.GetBytesFromBase64(),
			sbyte => element.GetSByte(),
			byte => element.GetByte(),
			short => element.GetInt16(),
			ushort => element.GetUInt16(),
			int => element.GetInt32(),
			uint => element.GetUInt32(),
			long => element.GetInt64(),
			ulong => element.GetUInt64(),
			double => element.GetDouble(),
			float => element.GetSingle(),
			decimal => element.GetDecimal(),
			DateTime => element.GetDateTime(),
			DateTimeOffset => element.GetDateTimeOffset(),
			Guid => element.GetGuid(),
			null => element.GetRawText(),
			_ => throw new Exception("Unbekannter Typ")
		};
		return (T) o;
	}
}

public static class XmlExtensions
{
	public static T Deserialize<T>(this XmlSerializer xml, string filePath)
	{
		using StreamReader sr = new StreamReader(filePath);
		object o = xml.Deserialize(sr);
		return (T) o;
	}

	public static T GetProperty<T>(this XmlNode node, string propName, bool isAttribute = false)
	{
		string text = isAttribute ? node.Attributes[propName].InnerText : node[propName].InnerText;
		object o = default(T) switch
		{
			int => int.Parse(text),
			//...
			_ => throw new Exception()
		};
		return (T) o;
	}
}