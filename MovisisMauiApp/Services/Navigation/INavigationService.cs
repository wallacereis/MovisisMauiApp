namespace MovisisMauiApp.Services.Navigation
{
    public interface INavigationService
    {
        Task NavigateToChildPageAsync(string pageName);
        Task NavigateToChildPageAsync(string pageName, string param);
        Task NavigateToRootAsync(int route);
        Task GoBackAsync();
    }
}
