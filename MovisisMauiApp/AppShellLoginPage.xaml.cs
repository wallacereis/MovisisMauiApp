namespace MovisisMauiApp;

public partial class AppShellLoginPage : Shell
{
	public AppShellLoginPage()
	{
		InitializeComponent();
        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        /*---------- Routes Onboardings ----------*/
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
    }

}