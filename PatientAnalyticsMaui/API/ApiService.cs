using RestSharp;
using PatientAnalyticsMaui.Models.Payload;
using PatientAnalyticsMaui.Models.Response;

namespace PatientAnalyticsMaui.API;

public class ApiService 
{
  private readonly RestClient _client;
  private string? _userToken;

  public ApiService()
  {
    _client = new RestClient(new RestClientOptions("http://localhost:5272")
    {
      ThrowOnAnyError = true,
      MaxTimeout = 1000,
    });

    _client.AddDefaultHeader("Content-Type", "application/json");
    _client.AddDefaultHeader("Accept", "application/json");

    _userToken = null;
  }

  public async Task<UserResponse> Login(LoginPayload payload)
  {
    var request = new RestRequest("/auth/login").AddJsonBody(payload);

    var response = await _client.PostAsync<UserResponse>(request);

    _userToken = response.Token;

    return response;
  }
}
