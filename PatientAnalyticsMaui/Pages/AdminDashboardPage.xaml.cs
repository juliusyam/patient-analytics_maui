using System.Collections.ObjectModel;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.API;
using PatientAnalyticsMaui.Models;
using PatientAnalyticsMaui.ViewModels;

namespace PatientAnalyticsMaui.Pages;

public partial class AdminDashboardPage : ContentPage
{
    private readonly MainPage _mainPage;
    private readonly ApiService _apiService;
    private readonly UserViewModel _userViewModel;
    private readonly AdminDashboardViewModel _adminDashboardViewModel;
    private readonly HubConnection? _hubConnection;
    private readonly IConfiguration _config;

    public AdminDashboardPage(AdminDashboardViewModel adminDashboardViewModel, UserViewModel userViewModel, IConfiguration config, MainPage mainPage)
    {
        InitializeComponent();

        _adminDashboardViewModel = adminDashboardViewModel;
        _userViewModel = userViewModel;
        _mainPage = mainPage;

        BindingContext = _adminDashboardViewModel;

        _hubConnection = _adminDashboardViewModel.HubConnection;
        
        _config = config;

        _apiService = new ApiService(_adminDashboardViewModel.Token, _adminDashboardViewModel.RefreshToken, _config);
        
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

    private async void OnViewSuperAdmins(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync(nameof(SuperAdminUsersPage));
    }
    

    private async void OnViewAdmins(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync(nameof(AdminUsersPage));
    }

    private async void OnViewDoctors(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync(nameof(DoctorUsersPage));
    }
}
