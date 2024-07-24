using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;
using PatientAnalyticsMaui.Models;

namespace PatientAnalyticsMaui.ViewModels;

public partial class DoctorDashboardViewModel : ObservableObject
{
    private UserViewModel _userViewModel;
  
    public DoctorDashboardViewModel(UserViewModel userViewModel)
    {
        _userViewModel = userViewModel;

        Patients = new ObservableCollection<Patient>();
        HubMessage = "Hub built";
        HubStatus = string.Empty;

        HubConnection = _userViewModel.GetHubConnection();
    
        if (HubConnection is not null) HubStatus = HubConnection.State.ToString();
        
        HubConnection?.On<string>("TestReceiveMessage", message =>
        {
            Console.WriteLine("Message received In Dashboard VM: " + message);
            HubMessage = message;
        });

        HubConnection?.On<Patient>("ReceiveNewPatient", patient =>
        {
            Console.WriteLine("Patient received: " + patient.Email);
            Patients.Add(patient);
        });
        
        HubConnection?.On<Patient>("ReceiveUpdatedPatient", patient =>
        {
            Console.WriteLine("Patient Update received: " + patient.Email);
            var existingPatient = Patients.FirstOrDefault(p => p.Id == patient.Id);

            if (existingPatient is not null)
            {
                var index = Patients.IndexOf(existingPatient);
                if (index >= 0) Patients[index] = patient;
            }
        });
        
        HubConnection?.On<Patient>("ReceiveDeletedPatient", (patient) =>
        {
            var existingPatient = Patients.FirstOrDefault(p => p.Id == patient.Id);

            if (existingPatient is not null) Patients.Remove(existingPatient);
        });

        Username = userViewModel.User?.Username;

        Token = userViewModel.Token;

        RefreshToken = userViewModel.RefreshToken;
    }

    [ObservableProperty]
    public ObservableCollection<Patient> patients = new();

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
}
