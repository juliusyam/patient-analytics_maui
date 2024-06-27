using PatientAnalyticsMaui.Models;
using PatientAnalyticsMaui.ViewModels;

namespace PatientAnalyticsMaui.Pages;

public partial class PatientPage : ContentPage
{
  private PatientViewModel _patientViewModel;

	public PatientPage(PatientViewModel patientViewModel)
	{
		InitializeComponent();

        BindingContext = patientViewModel;

        _patientViewModel = patientViewModel;
	}

    private async void OnNavigatePatientEdit(object sender, EventArgs e)
    {
        var patient = ((VisualElement)sender).BindingContext as Patient;

        await AppShell.Current.GoToAsync(nameof(PatientEditPage), true, new Dictionary<string, object>
        {
			{ "Patient", _patientViewModel.Patient }
		});
	}
}
