using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.API;
using PatientAnalyticsMaui.ViewModels;
using PatientAnalyticsMaui.Resources.Localization;
using Microsoft.Extensions.Localization;

namespace PatientAnalyticsMaui.Pages;

public partial class PatientEditPage : ContentPage
{
	string genderInput = "";
	string firstNameInput = "";
	string lastnameInput = "";
    string emailInput = "";
    string addressInput = "";
    DateTime dateOfBirthInput;

    private readonly PatientViewModel _patientViewModel;
	private readonly ApiService _apiService;
	private readonly IConfiguration _config;
    private readonly IStringLocalizer<Localized> _localized;

    public PatientEditPage(PatientViewModel patientViewModel, IConfiguration config, IStringLocalizer<Localized> localized)
	{
		InitializeComponent();

		BindingContext = patientViewModel;

		_patientViewModel = patientViewModel;

		_config = config;

		_apiService = new ApiService(patientViewModel.Token, patientViewModel.RefreshToken, _config);
	}

	private void OnGenderInputChange(object sender, EventArgs e)
	{
        genderInput = ((Entry)sender).Text;
	}

	private void OnFirstNameInputChange(object sender, EventArgs e)
	{
		firstNameInput = ((Entry)sender).Text;
	}

	private void OnLastNameInputChange(object sender, EventArgs e)
	{
		lastnameInput = ((Entry)sender).Text;
	}

    private void OnEmailInputChange(object sender, EventArgs e)
    {
        emailInput = ((Entry)sender).Text;
    }

    private void OnAddressInputChange(object sender, EventArgs e)
    {
        addressInput = ((Entry)sender).Text;
    }

    private void OnDateOfBirthSelected(object sender, EventArgs e)
    {
        if (sender is DatePicker datePicker)
        {
            dateOfBirthInput = datePicker.Date;
        }
    }

    private async void OnEditPatient(object sender, EventArgs e)
	{
		var payload = _patientViewModel.Patient;

		payload.Gender = genderInput;
		payload.FirstName = firstNameInput;
		payload.LastName = lastnameInput;
		payload.Email = emailInput;
		payload.DateOfBirth = dateOfBirthInput;

		try
		{
			var response = await _apiService.EditPatient(payload);

			_patientViewModel.Patient = response;

			await AppShell.Current.GoToAsync(nameof(PatientPage), true, new Dictionary<string, object>
			{
				{ "Patient", _patientViewModel.Patient }
			});

			Navigation.RemovePage(this);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			
			await DisplayAlert("Unable to Edit", ex.Message, "OK");
		}
	}
}
