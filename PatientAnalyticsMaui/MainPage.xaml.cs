using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.Models.Payload;
using PatientAnalyticsMaui.API;
using PatientAnalyticsMaui.Pages;
using PatientAnalyticsMaui.ViewModels;

namespace PatientAnalyticsMaui;

public partial class MainPage : ContentPage
{
	string username = "";
	string password = "";

	private readonly ApiService _apiService;
	private readonly UserViewModel _userViewModel;
	private readonly IConfiguration _config;

	public MainPage(UserViewModel userViewModel, IConfiguration config)
	{
		InitializeComponent();
		BindingContext = userViewModel;
		_userViewModel = userViewModel;
		_config = config;

		_apiService = new ApiService(userViewModel.Token, _config);
	}

	private async void OnUsernameInputChanged(object sender, EventArgs e)
	{
		username = ((Entry)sender).Text;
		ValidateInput();
	}

	private async void OnPasswordInputChanged(object sender, EventArgs e)
	{
		password = ((Entry)sender).Text;
		ValidateInput();
	}

	private void ValidateInput()
	{
		if (password.Length > 4 && username.Length > 1)
		{
			IsInputValid = true;
		} 
		else
		{
			IsInputValid = false;
		}
	}

	private async void OnLogin(object sender, EventArgs e)
	{
		try
		{
			var response = await _apiService.Login(new LoginPayload(username, password));

			_userViewModel.DefineUser(response.User, response.Token);
		}
		catch (Exception exception)
		{
			Console.WriteLine(exception);
		
			await DisplayAlert("Unable to login", "Please try again with a different username and password combo", "OK");
		}
	}

	private async void OnLogout(object sender, EventArgs e)
	{
		_userViewModel.RemoveUser();
	}

	private async void ToPatientDashboard(object sender, EventArgs e)
	{ 
		await AppShell.Current.GoToAsync(nameof(DashboardPage)); 
	}

	public bool IsInputValid { get; private set; }
}
