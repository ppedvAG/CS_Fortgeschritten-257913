namespace LinqErweiterungsmethoden;

internal static class Erweiterungsmethoden
{
	public static int Quersumme(this int zahl) //this: Typ auf den die Methode gehängt werden soll
	{
		//int summe = 0;
		//string zahlAlsString = zahl.ToString();
		//for (int i = 0; i < zahlAlsString.Length; i++)
		//{
		//	summe += (int) char.GetNumericValue(zahlAlsString[i]);
		//}
		//return summe;

		return (int) zahl.ToString().Sum(char.GetNumericValue);
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
	{
		return list.OrderBy(e => Random.Shared.Next());
	}
}