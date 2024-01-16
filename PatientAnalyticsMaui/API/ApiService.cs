using RestSharp;
using PatientAnalyticsMaui.Models.Payload;
using PatientAnalyticsMaui.Models.Response;
using PatientAnalyticsMaui.ViewModels;

namespace PatientAnalyticsMaui.API;

public class ApiService
{
  private readonly RestClient _client;
  private readonly UserViewModel _userViewModel;

  public ApiService(UserViewModel userViewModel)
  {
    _client = new RestClient(new RestClientOptions("http://localhost:5272")
    {
      ThrowOnAnyError = true,
      MaxTimeout = 1000,
    });

    _client.AddDefaultHeader("Content-Type", "application/json");
    _client.AddDefaultHeader("Accept", "application/json");

    _userViewModel = userViewModel;
  }

  public async Task<UserResponse> Login(LoginPayload payload)
  {
    var request = new RestRequest("/auth/login").AddJsonBody(payload);

    var response = await _client.PostAsync<UserResponse>(request);

    _userViewModel.DefineUser(response.User, response.Token);

    return response;
  }
}
