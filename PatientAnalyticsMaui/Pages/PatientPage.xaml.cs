using PatientAnalyticsMaui.Models;
using PatientAnalyticsMaui.ViewModels;
using PatientAnalyticsMaui.API;
using Microsoft.Extensions.Configuration;

namespace PatientAnalyticsMaui.Pages;

public partial class PatientPage : ContentPage
{
    private PatientViewModel _patientViewModel;
    private readonly ApiService _apiService;
    private readonly IConfiguration _config;

    public PatientPage(PatientViewModel patientViewModel, IConfiguration config)
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

    private async void OnDeletePatient(object sender, EventArgs e)
    {
        bool confirmDelete = await DisplayAlert("Confirm Deletion", "Are you sure you want to delete this patient?", "Delete", "Cancel");

        if (confirmDelete)
        {
            var patientID = _patientViewModel.Patient.Id;
            bool success = await _apiService.DeletePatient(patientID);
            if (success)
            {
                await DisplayAlert("Success", "Patient has been deleted.", "OK");
                //await AppShell.Current.GoToAsync("/DoctorsPatientsPage", true);
                await AppShell.Current.GoToAsync(nameof(DoctorsPatientsPage));
                Navigation.RemovePage(this);
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete patient.", "OK");
            }
        }
    }
}
