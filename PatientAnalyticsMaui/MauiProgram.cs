using System.Reflection;
using Microsoft.Extensions.Logging;
using PatientAnalyticsMaui.Pages;
using PatientAnalyticsMaui.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;

namespace PatientAnalyticsMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var executingAssembly = Assembly.GetExecutingAssembly();
		using var stream = executingAssembly.GetManifestResourceStream("PatientAnalyticsMaui.appsettings.json")!;

		var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
		
		var builder = MauiApp.CreateBuilder();
	    
		builder
		    .UseMauiApp<App>()
		    .UseMauiCommunityToolkit()
		    .ConfigureFonts(fonts => 
		    {
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Configuration.AddConfiguration(config);

	    builder.Services.AddSingleton<UserViewModel>();
	    builder.Services.AddSingleton<PatientViewModel>();
	    builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<DoctorDashboardViewModel>();
        builder.Services.AddTransient<AdminDashboardViewModel>();
        builder.Services.AddTransient<DoctorDashboardPage>();
        builder.Services.AddTransient<DoctorsPatientsPage>();
        builder.Services.AddTransient<AdminDashboardPage>();
        builder.Services.AddTransient<SuperAdminUsersPage>();
        builder.Services.AddTransient<AdminUsersPage>();
        builder.Services.AddTransient<DoctorUsersPage>();

        builder.Services.AddTransient<PatientPage>();
	    builder.Services.AddTransient<PatientEditPage>();

        builder.Services.AddLocalization();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

