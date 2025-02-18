using Plugin.Maui.Biometric;

namespace MovisisMauiApp.ViewModels
{
    public partial class StartViewModel : BaseViewModel
    {
        #region PRIVATE PROPERTIES        
        private readonly INavigationService _navigationService;
        #endregion

        #region CONSTRUCTOR        
        public StartViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;                
        }
        #endregion

        #region RELAY COMMANDS
        [RelayCommand]
        private async Task LoginAsync() => await _navigationService.NavigateToRootAsync((int)AppShellType.LoginPage);
        [RelayCommand]
        private async Task FingerPrintAsync()
        {
            try
            {
                IsBusy = true;

                var result = await BiometricAuthenticationService.Default.AuthenticateAsync(new AuthenticationRequest()
                {
                    Title = "Por favor, autenticar para continuar!",
                    NegativeText = "Cancelar autenticação"
                }, CancellationToken.None);

                if (result.Status == BiometricResponseStatus.Failure)
                {
                    await Toast.Make($"Necessário ativar impressão digital!", ToastDuration.Long).Show();
                    return;
                }

                if (result.Status == BiometricResponseStatus.Success)
                {
                    await _navigationService.NavigateToRootAsync((int)AppShellType.HomePage);
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"Atenção! Não foi possível obter a impressão digital!\n {ex.Message}", ToastDuration.Long).Show();
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
