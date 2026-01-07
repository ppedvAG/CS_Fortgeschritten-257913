namespace Events;

internal class Component
{
	public event Action Start; //Beispiel: Action statt EventHandler

	public event Action End;

	public event Action<int> Progress;

	public void Run()
	{
		Start?.Invoke();

		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(100);
			Progress?.Invoke(i); //Hier wird der Fortschritt weitergegeben
		}

		End?.Invoke();
	}
}
