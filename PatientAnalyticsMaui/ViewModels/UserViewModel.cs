using PatientAnalyticsMaui.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PatientAnalyticsMaui.ViewModels;

public partial class UserViewModel : ObservableObject
{
  public UserViewModel()
  {
    User = null;
    Token = null;
  }

  public void DefineUser(User user, string token)
  {
    User = user;
    Token = token;
  }

  public void RemoveUser()
  {
    User = null;
    Token = null;
  }

#nullable enable
  private User? user = null;
  public User? User
  {
    get => user;
    set
    {
      user = value;
      OnPropertyChanged();

      IsLoggedIn = (value != null);
      OnPropertyChanged();

      IsNotLoggedIn = (value == null);
      OnPropertyChanged();

      IsDoctor = value?.Role == "Doctor";
      OnPropertyChanged();
    }
  }

  [ObservableProperty]
  public bool isLoggedIn;

  [ObservableProperty]
  public bool isNotLoggedIn;

  [ObservableProperty]
  public bool isDoctor;

#nullable enable
  [ObservableProperty]
  public string? token;
}
