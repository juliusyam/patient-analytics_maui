using System.Text.Json.Serialization;

namespace PatientAnalyticsMaui.Models.Response;

public record UserResponse
{
    [JsonPropertyName("token")]
    public string Token { get; init; }

    [JsonPropertyName("refreshtoken")]
    public string RefreshToken { get; init; }

    [JsonPropertyName("user")]
    public User User { get; init; }
}
