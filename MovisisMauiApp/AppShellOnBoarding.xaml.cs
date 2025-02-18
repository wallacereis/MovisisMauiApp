namespace MovisisMauiApp;

public partial class AppShellOnBoarding : Shell
{
	public AppShellOnBoarding()
	{
		InitializeComponent();
        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        /*---------- Routes Onboardings ----------*/
        Routing.RegisterRoute(nameof(StartPage), typeof(StartPage));
    }
}