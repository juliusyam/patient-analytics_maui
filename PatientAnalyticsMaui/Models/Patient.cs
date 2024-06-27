using System.Text.Json.Serialization;

namespace PatientAnalyticsMaui.Models;

public record Patient : Person
{
  [JsonPropertyName("doctorId")]
  public int DoctorId { get; set; }
}
