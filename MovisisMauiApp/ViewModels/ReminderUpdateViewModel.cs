namespace MovisisMauiApp.ViewModels
{
    [QueryProperty(nameof(Reminder), "ReminderObject")]
    public partial class ReminderUpdateViewModel : BaseViewModel
    {
        #region PRIVATE PROPERTIES
        private readonly IReminderService _service;
        private readonly IReminderAttachmentService _attachmentService;
        private readonly INavigationService _navigationService;
        #endregion

        #region CONSTRUCTOR
        public ReminderUpdateViewModel(IReminderService service, IReminderAttachmentService attachmentService, INavigationService navigationService)
        {
            _service = service;
            _attachmentService = attachmentService;
            _navigationService = navigationService;
        }
        #endregion

        #region OBSERVABLE PROPERTIES        
        [ObservableProperty]
        private Reminder _reminder;
        [ObservableProperty]
        private TimeSpan _selectedTime;
        [ObservableProperty]
        private DateTime _selectedDate;
        [ObservableProperty]
        private string _attachmentFileName;
        #endregion

        #region RELAY COMMANDS
        [RelayCommand]
        private async Task ReturnAsync() => await _navigationService.NavigateToRootAsync((int)AppShellType.HomePage);

        [RelayCommand]
        private async Task ReminderUpdateAsync()
        {
            try
            {
                IsBusy = true;
                var confirm = await Shell.Current.DisplayAlert("Atenção!", "Confirma atualização?", "Confirmar", "Cancelar");
                if (confirm)
                {
                    await _service.InitializeAsync();
                    _reminder.ExpirationDate = SelectedDate + SelectedTime;
                    await _service.UpdateItemAsync(_reminder);

                    // Attach Images
                    if (!string.IsNullOrWhiteSpace(AttachmentFileName))
                        await ReminderAttachImagesAsync(_reminder);

                    await Toast.Make($"Lembrete atualizado com sucesso!", ToastDuration.Long).Show();
                    await _navigationService.NavigateToRootAsync((int)AppShellType.HomePage);
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"Atenção! Não foi possível atualizar o lembrete!\n {ex.Message}", ToastDuration.Long).Show();
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
            SelectedDate = _reminder.ExpirationDate.Date;
            SelectedTime = new TimeSpan(_reminder.ExpirationDate.Hour, _reminder.ExpirationDate.Minute, _reminder.ExpirationDate.Second);
            AttachmentFileName = 
                _reminder.ReminderAttachments.Any() ? 
                _reminder.ReminderAttachments.First().AttachmentFileName : 
                string.Empty;
        }
        #endregion

        #region PRIVATE METHODS
        private async Task ReminderAttachImagesAsync(Reminder reminder)
        {
            // First Remove attachments
            await _attachmentService.InitializeAsync();
            var attachments = await _attachmentService.GetAllByReminderIdAsync(reminder.ReminderId);
            if (attachments.Any())
            {
                foreach (var attachment in attachments)
                {
                    await _attachmentService.DeleteItemAsync(attachment);
                }
            }
            // Register attachments
            var reminderAttach = new ReminderAttachment
            {
                ReminderId = reminder.ReminderId,
                AttachmentFileName = _attachmentFileName,
                CreationDate = DateTime.Now
            };
            await _attachmentService.InitializeAsync();
            await _attachmentService.CreateItemAsync(reminderAttach);
        }
        #endregion
    }
}
