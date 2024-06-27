using System;
using CommunityToolkit.Mvvm.ComponentModel;
using PatientAnalyticsMaui.API;
using PatientAnalyticsMaui.Models;

namespace PatientAnalyticsMaui.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
  private readonly UserViewModel _userViewModel;

  public DashboardViewModel(UserViewModel userViewModel)
  {
    _userViewModel = userViewModel;

    Patients = new List<Patient>();

    Username = userViewModel.User.Username;

    Token = userViewModel.Token;
  }

  [ObservableProperty]
  public List<Patient> patients;

  [ObservableProperty]
  public string? username;

  [ObservableProperty]
  public string? token;
}
