using System.Text.Json.Serialization;

namespace PatientAnalyticsMaui.Models;

public record User : Person
{
  //private User(
  //    DateTime dateOfBirth, string gender, string email, string username, string role, string? address, string? firstName, string? lastName, DateTime dateCreated, DateTime? dateEdited
  //) : base(dateOfBirth, gender, email, address, firstName, lastName, dateCreated, dateEdited)
  //{
  //  Username = username;
  //  Role = role;
  //}

  [JsonPropertyName("username")]
  public string Username { get; init; }

  [JsonPropertyName("role")]
  public string Role { get; init; }
}


