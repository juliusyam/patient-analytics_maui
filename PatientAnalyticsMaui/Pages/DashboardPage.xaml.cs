using System.Collections.ObjectModel;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.API;
using PatientAnalyticsMaui.Models;
using PatientAnalyticsMaui.ViewModels;

namespace PatientAnalyticsMaui.Pages;

public partial class DashboardPage : ContentPage
{
    private readonly ApiService _apiService;
    private readonly DashboardViewModel _dashboardViewModel;
    private readonly HubConnection? _hubConnection;
    private readonly IConfiguration _config;

    public DashboardPage(DashboardViewModel dashboardViewModel, IConfiguration config)
    {
        InitializeComponent();

        _dashboardViewModel = dashboardViewModel;

        BindingContext = _dashboardViewModel;

        _hubConnection = _dashboardViewModel.HubConnection;
        
        _config = config;

        _apiService = new ApiService(dashboardViewModel.Token, _config);

        OnFetchPatients();
    }

    private async void OnFetchPatients()
    {
        try
        {
            var patients = await _apiService.GetPatients();
            _dashboardViewModel.Patients = new ObservableCollection<Patient>(patients);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            Console.WriteLine($"HTTP request exception: {exception.Message}");
            await DisplayAlert("Unable to get patients", exception.ToString(), "OK");
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

    private async void OnSendMessage(object sender, EventArgs e)
    {
        if (_hubConnection is not null)
            await _hubConnection.InvokeAsync("TestSendMessage", "Mock Message from Maui");
    }
}
