using PatientAnalyticsMaui.Models;

namespace PatientAnalyticsMaui.Controls;

public partial class UserCollectionView : ContentView
{
    public UserCollectionView()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty UsersProperty =
            BindableProperty.Create(nameof(Users), typeof(IEnumerable<User>), typeof(UserCollectionView), null);

    public IEnumerable<User> Users
    {
        get => (IEnumerable<User>)GetValue(UsersProperty);
        set => SetValue(UsersProperty, value);
    }
}