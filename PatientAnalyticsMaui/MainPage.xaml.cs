using RestSharp;
using PatientAnalyticsMaui.Models.Payload;
using PatientAnalyticsMaui.API;
using PatientAnalyticsMaui.ViewModels;

namespace PatientAnalyticsMaui;

public partial class MainPage : ContentPage
{
	int count = 0;
	private readonly ApiService _apiService;
	private readonly UserViewModel _userViewModel;

	public MainPage(UserViewModel userViewModel)
	{
		InitializeComponent();
    BindingContext = userViewModel;
		_userViewModel = userViewModel;

    _apiService = new ApiService(userViewModel);
  }

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);

		var response = await _apiService.Login(new LoginPayload("juliusyam_admin1", "RuleBritanniaGodSaveTheQueen123!"));

		CounterBtn.Text = $"You are now logged in as {response.User.Username}";
  }

	private async void OnLogout(object sender, EventArgs e)
	{
		_userViewModel.RemoveUser();

		CounterBtn.Text = "Login";
	}
}


