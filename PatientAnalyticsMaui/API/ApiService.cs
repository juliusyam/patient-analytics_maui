using Microsoft.Extensions.Configuration;
using PatientAnalyticsMaui.Models;
using PatientAnalyticsMaui.Models.Payload;
using PatientAnalyticsMaui.Models.Response;
using RestSharp;

namespace PatientAnalyticsMaui.API;

public class ApiService
{ 
    private readonly RestClient _client;
    private readonly IConfiguration _config;
    private readonly string _token;

    public ApiService(string token, IConfiguration config)
    {
        _token = token;
        _config = config;
        
        _client = new RestClient(new RestClientOptions(_config.GetRequiredSection("Api").Get<ApiConfig>().BaseUrl)
        {
            ThrowOnAnyError = true,
            MaxTimeout = 10000,
        });
        
        _client.AddDefaultHeader("Accept", "application/json");

        if (token is not null)
        {
            _client.AddDefaultHeader("Authorization", $"Bearer { token }");
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
}
