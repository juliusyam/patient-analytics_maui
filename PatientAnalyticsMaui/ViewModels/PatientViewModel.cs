using System;
using PatientAnalyticsMaui.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PatientAnalyticsMaui.ViewModels;

[QueryProperty(nameof(Patient), "Patient")]
public partial class PatientViewModel : ObservableObject
{
    public PatientViewModel(UserViewModel userViewModel)
    {
        Token = userViewModel.Token;
        RefreshToken = userViewModel.RefreshToken;
    }

    [ObservableProperty]
    public Patient patient;

    [ObservableProperty]
    public string? token;

    [ObservableProperty]
    public string? refreshToken;
}

