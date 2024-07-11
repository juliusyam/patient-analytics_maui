using System.Text.Json;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.Models;

namespace PatientAnalyticsMaui.ViewModels;

public partial class UserViewModel : ObservableObject
{
    private readonly IConfiguration _config;
    public UserViewModel(IConfiguration config)
    {
        User = null;
        Token = null;
        _config = config;
    }

    public void DefineUser(User user, string token)
    {
        User = user;
        Token = token;
    }

    public void RemoveUser()
    {
        User = null;
        Token = null;
    }

    #nullable enable
    private User? user;
    public User? User
    {
        get => user;
        set
        {
            user = value;
            OnPropertyChanged();

            IsLoggedIn = (value != null);
            OnPropertyChanged();

            IsNotLoggedIn = (value == null);
            OnPropertyChanged();

            IsDoctor = value?.Role == "Doctor";
            OnPropertyChanged();
        }
    }

    [ObservableProperty]
    public bool isLoggedIn;

    [ObservableProperty]
    public bool isNotLoggedIn;

    [ObservableProperty]
    public bool isDoctor;

    #nullable enable
    private string? token;

    public string? Token
    {
        get => token;
        set
        {
            token = value;
            if (token is not null) EstablishHubConnection();
        }
    }
    
    private HubConnection? _hubConnection;

    private async Task EstablishHubConnection()
    {
        if (_hubConnection is not null) await _hubConnection.DisposeAsync();
        
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_config.GetRequiredSection("HubConnection").Get<HubConnectionConfig>()!.Url, options =>
            {
                options.SkipNegotiation = true;
                options.Transports = HttpTransportType.WebSockets;
                options.AccessTokenProvider = () => Task.FromResult(Token);
            })
            .AddJsonProtocol(protocolOptions =>
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                jsonOptions.Converters.Add(new JsonStringEnumConverter());

                protocolOptions.PayloadSerializerOptions = jsonOptions;
            })
            .Build();

        await _hubConnection.StartAsync();
    }

    public HubConnection? GetHubConnection() => _hubConnection;
}
