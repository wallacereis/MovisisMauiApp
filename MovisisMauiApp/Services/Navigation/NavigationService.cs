namespace MovisisMauiApp.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        string route;

        public NavigationService()
        {
            route = string.Empty;
        }

        public async Task NavigateToChildPageAsync(string pageName)
        {
            route = $"{Shell.Current.CurrentState.Location}/{pageName}";
            await Shell.Current.GoToAsync(pageName);
        }

        public async Task NavigateToChildPageAsync(string pageName, string param)
        {
            route = $"{Shell.Current.CurrentState.Location}/{pageName}";
            await Shell.Current.GoToAsync($"{route}?{param}");
        }

        public Task NavigateToRootAsync(int route)
        {
            App.Current.MainPage = route switch
            {
                (int)AppShellType.OnboardingPage => new AppShellOnBoarding(),
                (int)AppShellType.LoginPage => new AppShellLoginPage(),
                (int)AppShellType.HomePage => new AppShellHome(),
                _ => new AppShellOnBoarding()
            };
            return Task.CompletedTask;
        }

        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
