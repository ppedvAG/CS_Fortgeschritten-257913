namespace AsyncAwaitWPF;

public class AsyncDataSource
{
	public async IAsyncEnumerable<int> Generate()
	{
		while (true)
		{
			await Task.Delay(Random.Shared.Next(100, 1000));
			yield return Random.Shared.Next(); //yield return: Gibt den nächsten in der Sequenz zurück
		}
	}
}