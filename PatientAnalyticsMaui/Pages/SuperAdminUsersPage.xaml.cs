using System.Collections.ObjectModel;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.API;
using PatientAnalyticsMaui.Models;
using PatientAnalyticsMaui.ViewModels;

namespace PatientAnalyticsMaui.Pages;

public partial class SuperAdminUsersPage : ContentPage
{
    private readonly MainPage _mainPage;
    private readonly ApiService _apiService;
    private readonly AdminDashboardViewModel _adminDashboardViewModel;
    private readonly HubConnection? _hubConnection;
    private readonly IConfiguration _config;

    public SuperAdminUsersPage(AdminDashboardViewModel adminDashboardViewModel, IConfiguration config, MainPage mainPage)
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
            var superAdmins = await _apiService.GetSuperAdmins();
            _adminDashboardViewModel.Users = new ObservableCollection<User>(superAdmins);
        }
        catch (Exception exception)
        {
            await DisplayAlert("Unable to get patients", exception.ToString(), "OK");
        }
    }

    private async void OnSendMessage(object sender, EventArgs e)
    {
        if (_hubConnection is not null)
            await _hubConnection.InvokeAsync("TestSendMessage", "Mock Message from Maui");
    }

    private async void OnLogout(object sender, EventArgs e)
    {
        _mainPage.ClearLoginFields();
        // TODO: add using nameof() instead of hardcoded
        await AppShell.Current.GoToAsync("///MainPage");
    }
}
