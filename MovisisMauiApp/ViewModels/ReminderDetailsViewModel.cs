namespace MovisisMauiApp.ViewModels
{
    [QueryProperty(nameof(Reminder), "ReminderObject")]
    public partial class ReminderDetailsViewModel : BaseViewModel
    {
        #region PRIVATE PROPERTIES
        private readonly IReminderService _service;
        private readonly INavigationService _navigationService;
        #endregion

        #region CONSTRUCTOR        
        public ReminderDetailsViewModel(IReminderService service, INavigationService navigationService)
        {
            _service = service;
            _navigationService = navigationService;
        }
        #endregion

        #region OBSERVABLE PROPERTIES        
        [ObservableProperty]
        private Reminder _reminder;
        [ObservableProperty]
        private string _attachmentFileName;
        #endregion

        #region RELAY COMMANDS
        [RelayCommand]
        private async Task ReminderUpdateAsync() =>
        await Shell.Current.GoToAsync("ReminderUpdatePage", new Dictionary<string, object>
        {
            {"ReminderObject", _reminder }
        });

        [RelayCommand]
        private async Task ReminderDeleteAsync()
        {
            try
            {
                IsBusy = true;
                var confirm = await Shell.Current.DisplayAlert("Atenção!", "Confirma remover?", "Confirmar", "Cancelar");
                if (confirm)
                {
                    await _service.InitializeAsync();
                    await _service.DeleteItemAsync(_reminder);
                    await Toast.Make($"Lembrete removido com sucesso!", ToastDuration.Long).Show();
                    await _navigationService.NavigateToRootAsync((int)AppShellType.HomePage);
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"Atenção! Não foi possível remover o lembrete!\n {ex.Message}", ToastDuration.Long).Show();
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task LoadReminderAsync()
        {
            AttachmentFileName =
                _reminder.ReminderAttachments.Any() ?
                _reminder.ReminderAttachments.First().AttachmentFileName :
                string.Empty;
        }
        #endregion
    }
}
