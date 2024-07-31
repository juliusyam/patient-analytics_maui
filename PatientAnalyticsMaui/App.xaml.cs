using System.Globalization;

namespace PatientAnalyticsMaui;

public partial class App : Application
{
	public App()
	{
	    InitializeComponent();

        var culture = CultureInfo.CurrentCulture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        MainPage = new AppShell();
	}
}

