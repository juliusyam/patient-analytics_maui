using System.Text.Json.Serialization;
namespace PatientAnalyticsMaui.Models.Response;

record UserResponse
{
  [JsonPropertyName("token")]
  public string Token { get; init; }

  [JsonPropertyName("user")]
  public User User { get; init; }
}
