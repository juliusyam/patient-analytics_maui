﻿using System.Collections.ObjectModel;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.API;
using PatientAnalyticsMaui.Models;
using PatientAnalyticsMaui.ViewModels;
using PatientAnalyticsMaui.Resources.Localization;
using Microsoft.Extensions.Localization;

namespace PatientAnalyticsMaui.Pages;

public partial class DoctorsPatientsPage : ContentPage
{
    private readonly MainPage _mainPage;
    private readonly ApiService _apiService;
    private readonly DoctorDashboardViewModel _doctorDashboardViewModel;
    private readonly HubConnection? _hubConnection;
    private readonly IConfiguration _config;
    private readonly IStringLocalizer<Localized> _localized;

    public DoctorsPatientsPage(DoctorDashboardViewModel doctorDashboardViewModel, IConfiguration config, MainPage mainPage, IStringLocalizer<Localized> localized)
    {
        InitializeComponent();

        _doctorDashboardViewModel = doctorDashboardViewModel;
        _mainPage = mainPage;
        BindingContext = _doctorDashboardViewModel;

        _hubConnection = _doctorDashboardViewModel.HubConnection;
        
        _config = config;

        _apiService = new ApiService(_doctorDashboardViewModel.Token, _doctorDashboardViewModel.RefreshToken, _config);

        OnFetchPatients();
    }

    private async void OnFetchPatients()
    {
        try
        {
            var patients = await _apiService.GetPatients();
            _doctorDashboardViewModel.Patients = new ObservableCollection<Patient>(patients);
        }
        catch (Exception exception)
        {
            await DisplayAlert(_localized["AuthError_UnableGetPatients"], exception.ToString(), _localized["Button_OK"]);
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

    private async void OnLogout(object sender, EventArgs e)
    {
        _mainPage.ClearLoginFields();
        // TODO: add using nameof() instead of hardcoded
        await AppShell.Current.GoToAsync("///MainPage");
    }
}
