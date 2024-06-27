using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.ViewModels;
using PatientAnalyticsMaui.Models;
using PatientAnalyticsMaui.API;

namespace PatientAnalyticsMaui.Pages;

public partial class DashboardPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly DashboardViewModel _dashboardViewModel;
    private readonly IConfiguration _config;

    public DashboardPage(DashboardViewModel dashboardViewModel, IConfiguration config)
    {
        InitializeComponent();

        _dashboardViewModel = dashboardViewModel;

        BindingContext = _dashboardViewModel;

        _config = config;

        _apiService = new ApiService(dashboardViewModel.Token, _config);

        OnFetchPatients();
    }

    private async void OnFetchPatients()
    {
        try
        { 
            _dashboardViewModel.Patients = await _apiService.GetPatients();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            await DisplayAlert("Unable to get patients", "Please try again later", "OK");
        }
    }

    private async void OnViewPatient(object sender, EventArgs e)
    {
        var patient = ((VisualElement)sender).BindingContext as Patient;

        await AppShell.Current.GoToAsync(nameof(PatientPage), true, new Dictionary<string, object>
        {
            { "Patient", patient }
        });
    }
}
