using PatientAnalyticsMaui.Models;
using System.ComponentModel;

namespace PatientAnalyticsMaui.ViewModels;

public class UserViewModel : INotifyPropertyChanged
{
  public UserViewModel()
  {
    User = null;
    Token = null;
  }

  public event PropertyChangedEventHandler PropertyChanged;
  public void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
  User? user = null;
  public User? User
  {
    get => user;
    set
    {
      user = value;
      OnPropertyChanged(nameof(User));

      IsLoggedIn = value != null;
      OnPropertyChanged(nameof(IsLoggedIn));
    }
  }

  public bool IsLoggedIn { get; private set; }

  #nullable enable
  public string? Token { get; private set; }
}
