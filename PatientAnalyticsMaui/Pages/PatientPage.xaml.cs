using PatientAnalyticsMaui.Models;
using PatientAnalyticsMaui.ViewModels;
using PatientAnalyticsMaui.API;
using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.Resources.Localization;
using Microsoft.Extensions.Localization;

namespace PatientAnalyticsMaui.Pages;

public partial class PatientPage : ContentPage
{
    private PatientViewModel _patientViewModel;
    private readonly ApiService _apiService;
    private readonly IConfiguration _config;
    private readonly IStringLocalizer<Localized> _localized;

    public PatientPage(PatientViewModel patientViewModel, IConfiguration config, IStringLocalizer<Localized> localized)
	{
		InitializeComponent();

        BindingContext = patientViewModel;

        _patientViewModel = patientViewModel;

        _config = config;

        _apiService = new ApiService(patientViewModel.Token, patientViewModel.RefreshToken, _config);
    }

    private async void OnNavigatePatientEdit(object sender, EventArgs e)
    {
        var patient = ((VisualElement)sender).BindingContext as Patient;

        await AppShell.Current.GoToAsync(nameof(PatientEditPage), true, new Dictionary<string, object>
        {
			{ "Patient", _patientViewModel.Patient }
		});
	}

    private async void OnNavigateDoctorPatientList(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync(nameof(DoctorsPatientsPage));
    }

    private async void OnDeletePatient(object sender, EventArgs e)
    {
        bool confirmDelete = await DisplayAlert(_localized["Title_DeleteConfirm"], _localized["Title_DeleteConfirm"], _localized["Button_Delete"], _localized["Button_Cancel"]);

        if (confirmDelete)
        {
            var patientID = _patientViewModel.Patient.Id;
            bool success = await _apiService.DeletePatient(patientID);
            if (success)
            {
                await DisplayAlert(_localized["Button_Success"], _localized["Label_DeleteSuccess"], _localized["Button_OK"]);
                await AppShell.Current.GoToAsync(nameof(DoctorsPatientsPage));
                //Navigation.RemovePage(this);
            }
            else
            {
                await DisplayAlert(_localized["Button_Error"], _localized["Label_DeleteError"], _localized["Button_OK"]);
            }
        }
    }
}
