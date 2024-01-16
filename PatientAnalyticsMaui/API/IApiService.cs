using PatientAnalyticsMaui.Models.Payload;
using PatientAnalyticsMaui.Models.Response;

namespace PatientAnalyticsMaui.API;

public interface IApiService
{
  public Task<UserResponse> Login(LoginPayload payload);
}
