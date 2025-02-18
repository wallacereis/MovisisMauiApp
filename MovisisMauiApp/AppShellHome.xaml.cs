namespace MovisisMauiApp;

public partial class AppShellHome : Shell
{
	public AppShellHome()
	{
		InitializeComponent();
        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        /*---------- Routes Tabbed Pages ----------*/
        Routing.RegisterRoute(nameof(RemindersPage), typeof(RemindersPage));
        Routing.RegisterRoute(nameof(ReminderDetailsPage), typeof(ReminderDetailsPage));
        Routing.RegisterRoute(nameof(ReminderCreatePage), typeof(ReminderCreatePage));
        Routing.RegisterRoute(nameof(ReminderUpdatePage), typeof(ReminderUpdatePage));
        Routing.RegisterRoute(nameof(RemindersCalendarPage), typeof(RemindersCalendarPage));
    }
}

