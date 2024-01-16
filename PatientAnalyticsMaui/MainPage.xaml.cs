using RestSharp;
using PatientAnalyticsMaui.Models.Payload;
using PatientAnalyticsMaui.Models.Response;

namespace PatientAnalyticsMaui;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();


  }

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);

    var client = new RestClient(new RestClientOptions("http://localhost:5272")
    {
      ThrowOnAnyError = true,
      MaxTimeout = 1000,
    });

    client.AddDefaultHeader("Content-Type", "application/json");
    client.AddDefaultHeader("Accept", "application/json");

    var request = new RestRequest("/auth/login")
      .AddJsonBody(new LoginPayload("juliusyam_admin1", "RuleBritanniaGodSaveTheQueen123!"));

    var response = await client.PostAsync<UserResponse>(request);

    Console.WriteLine(response.Token);
    Console.WriteLine(response.User);
  }
}


