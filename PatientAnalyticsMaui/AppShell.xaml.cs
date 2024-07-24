using PatientAnalyticsMaui.Pages;

namespace PatientAnalyticsMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(DoctorDashboardPage), typeof(DoctorDashboardPage)); 
		Routing.RegisterRoute(nameof(AdminDashboardPage), typeof(AdminDashboardPage)); 
		Routing.RegisterRoute(nameof(AdminUsersPage), typeof(AdminUsersPage)); 
		Routing.RegisterRoute(nameof(SuperAdminUsersPage), typeof(SuperAdminUsersPage)); 
		Routing.RegisterRoute(nameof(DoctorUsersPage), typeof(DoctorUsersPage)); 
		Routing.RegisterRoute(nameof(DoctorsPatientsPage), typeof(DoctorsPatientsPage)); 
		Routing.RegisterRoute(nameof(PatientPage), typeof(PatientPage)); 
		Routing.RegisterRoute(nameof(PatientEditPage), typeof(PatientEditPage));
  }
}

