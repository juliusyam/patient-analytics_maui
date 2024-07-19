using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.API;
using PatientAnalyticsMaui.ViewModels;

namespace PatientAnalyticsMaui.Pages;

public partial class PatientEditPage : ContentPage
{
	string emailInput = "";
	string firstNameInput = "";
	string lastnameInput = "";

	private readonly PatientViewModel _patientViewModel;
	private readonly ApiService _apiService;
	private readonly IConfiguration _config;

	public PatientEditPage(PatientViewModel patientViewModel, IConfiguration config)
	{
		InitializeComponent();

		BindingContext = patientViewModel;

		_patientViewModel = patientViewModel;

		_config = config;

		_apiService = new ApiService(patientViewModel.Token, patientViewModel.RefreshToken, _config);
	}

	private async void OnEmailInputChange(object sender, EventArgs e)
	{
		emailInput = ((Entry)sender).Text;
	}

	private async void OnFirstNameInputChange(object sender, EventArgs e)
	{
		firstNameInput = ((Entry)sender).Text;
	}

	private async void OnLastNameInputChange(object sender, EventArgs e)
	{
		lastnameInput = ((Entry)sender).Text;
	}

	private async void OnEditPatient(object sender, EventArgs e)
	{
		var payload = _patientViewModel.Patient;

		payload.FirstName = firstNameInput;
		payload.LastName = lastnameInput;
		payload.Email = emailInput;

		try
		{
			var response = await _apiService.EditPatient(payload);

			_patientViewModel.Patient = response;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			
			await DisplayAlert("Unable to Edit", ex.Message, "OK");
		}
	}
}
