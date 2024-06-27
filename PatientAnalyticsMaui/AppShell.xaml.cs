using PatientAnalyticsMaui.Pages;

namespace PatientAnalyticsMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
		Routing.RegisterRoute(nameof(PatientPage), typeof(PatientPage));
		Routing.RegisterRoute(nameof(PatientEditPage), typeof(PatientEditPage));
  }
}

