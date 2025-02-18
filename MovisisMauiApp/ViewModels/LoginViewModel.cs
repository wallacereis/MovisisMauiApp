namespace MovisisMauiApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        #region PRIVATE PROPERTIES
        private readonly INavigationService _navigationService;
        #endregion

        #region CONSTRUCTOR
        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        #endregion

        #region OBSERVABLE PROPERTIES        
        [ObservableProperty]
        private string _userName = string.Empty;
        [ObservableProperty]
        private string _userPassword = string.Empty;
        #endregion

        #region RELAY COMMANDS
        [RelayCommand]
        private async Task LoginAsync() 
        {
            try
            {
                if (UserName != "movisis" && UserPassword != "movi@123")
                {
                    await Toast.Make($"Login ou senha inválidos!\nPor favor, tente novamente!", ToastDuration.Long).Show();
                    return;
                }
                await _navigationService.NavigateToRootAsync((int)AppShellType.HomePage);
            }
            catch (Exception ex)
            {
                throw new Exception($"Atenção! Não foi possível efetuar o Login!\n{ex.Message}");
            }
        } 

        #endregion
    }
}
