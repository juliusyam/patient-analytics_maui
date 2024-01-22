using PatientAnalyticsMaui.Models.Payload;
using PatientAnalyticsMaui.API;
using PatientAnalyticsMaui.ViewModels;

namespace PatientAnalyticsMaui;

public partial class MainPage : ContentPage
{
	string username = "";
	string password = "";

	private readonly ApiService _apiService;
	private readonly UserViewModel _userViewModel;

  public MainPage(UserViewModel userViewModel)
	{
		InitializeComponent();
    BindingContext = userViewModel;
		_userViewModel = userViewModel;

    _apiService = new ApiService(userViewModel);
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
    } else
		{
			IsInputValid = false;
		};
  }

  private async void OnLogin(object sender, EventArgs e)
	{
		SemanticScreenReader.Announce(CounterBtn.Text);

    await _apiService.Login(new LoginPayload(username, password));
  }

	private async void OnLogout(object sender, EventArgs e)
	{
		_userViewModel.RemoveUser();
	}

	public bool IsInputValid { get; private set; }
}


