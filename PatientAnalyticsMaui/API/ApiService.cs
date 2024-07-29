using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.Models;
using PatientAnalyticsMaui.Models.Payload;
using PatientAnalyticsMaui.Models.Response;
using RestSharp;

namespace PatientAnalyticsMaui.API;

public class ApiService
{ 
    private RestClient _client;
    private readonly IConfiguration _config;
    private string _token;
    private string _refreshToken;

    public ApiService(string token, string refreshToken, IConfiguration config)
    {
        _token = token;
        _refreshToken = token;
        _config = config;
        InitializeClient();
    }

    private void InitializeClient()
    {
        _client = new RestClient(new RestClientOptions(_config.GetRequiredSection("Api").Get<ApiConfig>().BaseUrl)
        {
            ThrowOnAnyError = true,
            MaxTimeout = 10000,
        });

        _client.AddDefaultHeader("Accept", "application/json");

        if (!string.IsNullOrEmpty(_token))
        {
            _client.AddDefaultHeader("Authorization", $"Bearer {_token}");
        }
    }

    public async Task<UserResponse> Login(LoginPayload payload)
    {
        var request = new RestRequest("/api/auth/login").AddJsonBody(payload);
        
        var response = await _client.PostAsync<UserResponse>(request);

        return response;
    }

    // TODO: Query string implementation
    public async Task<List<Patient>> GetPatients()
    {
        var request = new RestRequest("/api/patients");

        var response = await _client.ExecuteAsync<List<Patient>>(request);

        return response.Data;
    }

    public async Task<Patient> EditPatient(Patient patientPayload)
    {
        var request = new RestRequest($"/api/patients/{ patientPayload.Id }").AddJsonBody(patientPayload);

        var response = await _client.PutAsync<Patient>(request);

        return response;
    }

    public async Task<bool> DeletePatient(int patientId)
    {
        var request = new RestRequest($"/api/patients/{patientId}");

        var response = await _client.DeleteAsync(request);

        return response.IsSuccessful;
    }

    public async Task Logout(string token, string refreshToken)
    {
        var request = new RestRequest("/api/auth/logout", Method.Delete);
                
        request.AddJsonBody(new LogoutPayload(refreshToken));
        try
        {
            var response = await _client.ExecuteAsync(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception during logout: " + ex.Message);
        }
        finally
        {
            _token = null;
            _refreshToken = null;
            InitializeClient();
        }
    }

    public async Task<List<User>> GetSuperAdmins()
    {
        var request = new RestRequest("/api/super-admins");

        var response = await _client.ExecuteAsync<List<User>>(request);

        return response.Data;
    }

    public async Task<List<User>> GetAdmins()
    {
        var request = new RestRequest("/api/admins");

        var response = await _client.ExecuteAsync<List<User>>(request);

        return response.Data;
    }

    public async Task<List<User>> GetDoctors()
    {
        var request = new RestRequest("/api/doctors");

        var response = await _client.ExecuteAsync<List<User>>(request);

        return response.Data;
    }
}
