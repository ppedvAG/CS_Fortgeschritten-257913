using System.Net.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		//Task.Run(() => Output1.Text = "OK");
		Task.Run(() => Dispatcher.Invoke(() => Output.Text = "OK")); //Dispatcher.Invoke: Lege diese Aufgabe auf den Main Thread
	}

	private async void Button_Click_1(object sender, RoutedEventArgs e)
	{
		string url = "http://www.gutenberg.org/files/54700/54700-0.txt";

		//Starten
		using HttpClient client = new HttpClient();
		Task<HttpResponseMessage> request = client.GetAsync(url); //Startet automatisch den GET-Request

		//Zwischenschritte
		Output.Text = "Text wird geladen...";
		ReqButton.IsEnabled = false;

		//Warten
		HttpResponseMessage response = await request;

		if (response.IsSuccessStatusCode)
		{
			//Starten
			Task<string> readTask = response.Content.ReadAsStringAsync();

			//Zwischenschritte
			Output.Text = "Text wird ausgelesen...";

			//Warten
			string text = await readTask;

			Output.Text = text;
		}
		ReqButton.IsEnabled = true;
	}

	private async void Button_Click_2(object sender, RoutedEventArgs e)
	{
		AsyncDataSource ads = new();
		await foreach (int x in ads.Generate())
		{
			Output.Text += x + "\n";
		}
	}
}