using System.Collections.ObjectModel;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.API;
using PatientAnalyticsMaui.Models;
using PatientAnalyticsMaui.ViewModels;

namespace PatientAnalyticsMaui.Pages;

public partial class DoctorUsersPage : ContentPage
{
    private readonly MainPage _mainPage;
    private readonly ApiService _apiService;
    private readonly AdminDashboardViewModel _adminDashboardViewModel;
    private readonly HubConnection? _hubConnection;
    private readonly IConfiguration _config;

    public DoctorUsersPage(AdminDashboardViewModel adminDashboardViewModel, IConfiguration config, MainPage mainPage)
	{
        InitializeComponent();

        _adminDashboardViewModel = adminDashboardViewModel;
        _mainPage = mainPage;
        BindingContext = _adminDashboardViewModel;

        _hubConnection = _adminDashboardViewModel.HubConnection;

        _config = config;

        _apiService = new ApiService(_adminDashboardViewModel.Token, _adminDashboardViewModel.RefreshToken, _config);

        OnFetchUsers();
    }

    private async void OnFetchUsers()
    {
        try
        {
            var admins = await _apiService.GetDoctors();
            _adminDashboardViewModel.Users = new ObservableCollection<User>(admins);
        }
        catch (Exception exception)
        {
            await DisplayAlert("Unable to get patients", exception.ToString(), "OK");
        }
    }

    private async void OnLogout(object sender, EventArgs e)
    {
        _mainPage.ClearLoginFields();
        // TODO: add using nameof() instead of hardcoded
        await AppShell.Current.GoToAsync("///MainPage");
    }
}