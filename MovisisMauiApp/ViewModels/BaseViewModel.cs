namespace MovisisMauiApp.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    public bool isBusy = false;
    public bool IsNotBusy => !IsBusy;

    /*---------------------- Xamarin Essentials Connectivity ----------------------*/
    public static bool InternetConnectionActive()
    {
        var profiles = Connectivity.ConnectionProfiles;
        if (profiles.Contains(ConnectionProfile.WiFi) || profiles.Contains(ConnectionProfile.Cellular))
            // Active Internet Connection.
            return true;
        else
            // No Internet Connection.
            return false;
    }
}
