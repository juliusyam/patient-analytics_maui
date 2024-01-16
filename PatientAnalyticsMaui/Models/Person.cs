using System.Text.Json.Serialization;

namespace PatientAnalyticsMaui.Models;

public record Person
{
  //public Person(DateTime dateOfBirth, string gender, string email, string? address, string? firstName, string? lastName, DateTime dateCreated,
  //      DateTime? dateEdited)
  //{
  //  DateOfBirth = dateOfBirth;
  //  Gender = gender;
  //  Email = email;
  //  Address = address;
  //  DateCreated = dateCreated;
  //  DateEdited = dateEdited;
  //  FirstName = firstName;
  //  LastName = lastName;
  //}

  [JsonPropertyName("id")]
  public int Id { get; init; }

  [JsonPropertyName("dateOfBirth")]
  public DateTime DateOfBirth { get; init; }

  [JsonPropertyName("gender")]
  public string Gender { get; init; }

  [JsonPropertyName("firstName")]
  public string? FirstName { get; init; }

  [JsonPropertyName("lastName")]
  public string? LastName { get; init; }

  [JsonPropertyName("email")]
  public string Email { get; init; }

  [JsonPropertyName("address")]
  public string? Address { get; init; }

  [JsonPropertyName("dateCreated")]
  public DateTime DateCreated { get; init; }

  [JsonPropertyName("dateEdited")]
  public DateTime? DateEdited { get; init; }
}


