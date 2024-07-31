using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.Models.Payload;
using PatientAnalyticsMaui.API;
using PatientAnalyticsMaui.Pages;
using PatientAnalyticsMaui.ViewModels;
using Microsoft.Extensions.Localization;
using PatientAnalyticsMaui.Resources.Localization;

namespace PatientAnalyticsMaui;

public partial class MainPage : ContentPage
{
	string username = "";
	string password = "";

	private readonly ApiService _apiService;
	private readonly UserViewModel _userViewModel;
	private readonly IConfiguration _config;
    private readonly IStringLocalizer<Localized> _localized;

    private bool _isPasswordVisible = false;

    public MainPage(UserViewModel userViewModel, IConfiguration config, IStringLocalizer<Localized> localized)
	{
		InitializeComponent();
		BindingContext = userViewModel;
		_userViewModel = userViewModel;
		_config = config;
		_localized = localized;

		_apiService = new ApiService(userViewModel.Token, userViewModel.RefreshToken, _config);
	}

    private void OnPasswordVisibilityToggleClicked(object sender, EventArgs e)
    {
        _isPasswordVisible = !_isPasswordVisible;
        passwordInput.IsPassword = !_isPasswordVisible;
        PasswordVisibilityToggle.Text = _isPasswordVisible ? "Hide" : "Show";
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
			
			await _userViewModel.DefineUser(response.User, response.Token);

			switch (response.User.Role)
			{
				case "Doctor":
					ToPatientDashboard();
					break;
				case "SuperAdmin":
				case "Admin":
					ToAdminDashboard();
					break;
				default:
					throw new InvalidOperationException($"Unexpected role: {response.User.Role}");
			}
		}
        catch (Exception exception)
        {
			if (exception.Message == "Request failed with status code Unauthorized")
			{
                await DisplayAlert(_localized["AuthError_LoginWrongPasswordTitle"], _localized["AuthError_LoginFailureMessage"], _localized["Button_OK"]);                
            }
			else
			{
                await DisplayAlert(_localized["AuthError_LoginUserNotFoundTitle"], _localized["AuthError_LoginFailureMessage"], _localized["Button_OK"]);
            }
            
        }
    }

	private async void ToPatientDashboard()
	{ 
		await AppShell.Current.GoToAsync(nameof(DoctorDashboardPage)); 
	}

    private async void ToAdminDashboard()
    {
        await AppShell.Current.GoToAsync(nameof(AdminDashboardPage));
    }

    public async void ClearLoginFields()
    {
        await _apiService.Logout(_userViewModel.Token, _userViewModel.RefreshToken);
        _userViewModel.RemoveUser();

        username = "";
        password = "";

        usernameInput.Text = "";
        passwordInput.Text = "";
    }

    public bool IsInputValid { get; private set; }
}
