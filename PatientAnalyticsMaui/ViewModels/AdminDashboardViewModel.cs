using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;
using PatientAnalyticsMaui.Models;

namespace PatientAnalyticsMaui.ViewModels;
public partial class AdminDashboardViewModel : ObservableObject
{
    private UserViewModel _userViewModel;

    public AdminDashboardViewModel(UserViewModel userViewModel)
    {
        _userViewModel = userViewModel;

        HubConnection = _userViewModel.GetHubConnection();

        HubMessage = "Hub built";

        if (HubConnection is not null) HubStatus = HubConnection.State.ToString();

        HubConnection?.On<string>("TestReceiveMessage", message =>
        {
            Console.WriteLine("Message received In Dashboard VM: " + message);
            HubMessage = message;
        });

        Username = userViewModel.User?.Username;

        Token = userViewModel.Token;

        RefreshToken = userViewModel.RefreshToken;

        IsSuperAdmin = _userViewModel.IsSuperAdmin;
    }

    [ObservableProperty]
    public ObservableCollection<User> users = new();

    [ObservableProperty]
    public string? username;

    [ObservableProperty]
    public string? token;

    [ObservableProperty]
    public string? refreshToken;

#nullable enable
    [ObservableProperty]
    public HubConnection? hubConnection;

    [ObservableProperty]
    public string hubMessage = "";

    [ObservableProperty]
    public string hubStatus = "";

    [ObservableProperty]
    public bool isSuperAdmin;

    [ObservableProperty]
    public bool isAdmin;

    [ObservableProperty]
    public bool isDoctor;
}
