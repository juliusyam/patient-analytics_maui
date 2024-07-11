namespace PatientAnalyticsMaui.Models;

public class ApiConfig
{
    public string BaseUrl { get; init; } = null!;
}

public class HubConnectionConfig
{
    public string Route { get; init; } = null!;
    public string Url { get; init; } = null!;
}