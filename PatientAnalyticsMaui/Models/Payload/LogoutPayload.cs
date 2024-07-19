using System;
namespace PatientAnalyticsMaui.Models.Payload;

public class LogoutPayload
{
    public LogoutPayload(string refreshToken)
    {
        RefreshToken = refreshToken;
    }

    public string RefreshToken { get; private set; }
}
