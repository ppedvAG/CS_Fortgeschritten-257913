using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();

		//Synchron
		//Toast();
		//Tasse();
		//Kaffee();
		//Console.WriteLine(sw.ElapsedMilliseconds);

		///////////////////////////////////////////////////////////

		//Parallel
		//Task toast = new Task(Toast);
		//Task tasse = new Task(Tasse);
		//Task kaffee = tasse.ContinueWith(v => Kaffee());

		//toast.ContinueWith(PrintMS);
		//kaffee.ContinueWith(PrintMS);

		//toast.Start();
		//tasse.Start();

		////Task.WaitAll(toast, tasse); //In der GUI nicht möglich
		////Console.WriteLine(sw.ElapsedMilliseconds);

		//void PrintMS(Task t)
		//{
		//	if (toast.IsCompletedSuccessfully && kaffee.IsCompletedSuccessfully)
		//		Console.WriteLine(sw.ElapsedMilliseconds);
		//}

		//Console.ReadKey();

		///////////////////////////////////////////////////////////

		//async und await
		//Wenn eine Methode await verwenden soll, muss diese async sein
		//Task toast = new Task(Toast);
		//toast.Start();

		//Task tasse = new Task(Tasse);
		//tasse.Start();

		//await tasse; //Warte (nicht-blockierend) auf die Tasse

		//Task kaffee = new Task(Kaffee); //Starte den Kaffee nach der Tasse
		//kaffee.Start();

		//await kaffee;
		//await toast;

		//Console.WriteLine(sw.ElapsedMilliseconds);

		///////////////////////////////////////////////////////////

		//Vereinfachungen
		//Task toast = Task.Run(Toast);
		//Task tasse = new Task(Tasse);
		//Task kaffee = tasse.ContinueWith(v => Kaffee()); //Starte den Kaffee nach der Tasse
		//tasse.Start();

		//await Task.WhenAll(toast, kaffee); //Äquivalent zu Task.WaitAll, aber async

		//Console.WriteLine(sw.ElapsedMilliseconds);

		///////////////////////////////////////////////////////////

		//Task toast = ToastAsync(); //Wenn eine async Methode ausgeführt wird, wird diese automatisch gestartet (als Task)
		//Task tasse = TasseAsync();
		//await tasse;
		//Task kaffee = KaffeeAsync();

		////Task.Run/t.Start() nicht mehr notwendig
		//await Task.WhenAll(toast, kaffee);
		//Console.WriteLine(sw.ElapsedMilliseconds);

		///////////////////////////////////////////////////////////

		//Async/Await mit Objekten
		//Task<Toast> toast = ToastObjectAsync();
		//Task<Tasse> tasse = TasseObjectAsync();
		//Tasse t = await tasse;
		//Task<Kaffee> kaffee = KaffeeObjectAsync(t);
		//Kaffee k = await kaffee;
		//Toast b = await toast;
		//Fruehstueck f = new Fruehstueck(b, k);
		//Console.WriteLine(sw.ElapsedMilliseconds);

		///////////////////////////////////////////////////////////

		//Vereinfachungen
		Task<Toast> toast = ToastObjectAsync();
		Task<Kaffee> kaffee = KaffeeObjectAsync(await TasseObjectAsync());
		Fruehstueck f = new Fruehstueck(await toast, await kaffee);
		Console.WriteLine(sw.ElapsedMilliseconds);
	}

	#region	Synchron
	static void Toast()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	static void Tasse()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Tasse fertig");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron
	static async Task ToastAsync()
	{
		//await Task.Run(() => Thread.Sleep(4000));
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
	}

	static async Task TasseAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
	}

	static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron mit Objekten
	static async Task<Toast> ToastObjectAsync()
	{
		//await Task.Run(() => Thread.Sleep(4000));
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Tasse> TasseObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee(t);
	}
	#endregion
}

public class Toast;

public class Tasse;

public record Kaffee(Tasse t);

public record Fruehstueck(Toast t, Kaffee k);