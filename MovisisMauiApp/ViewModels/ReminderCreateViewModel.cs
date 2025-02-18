namespace MovisisMauiApp.ViewModels
{
    public partial class ReminderCreateViewModel : BaseViewModel
    {
        #region PRIVATE PROPERTIES
        private readonly IReminderService _service;
        private readonly IReminderAttachmentService _attachmentService;
        private readonly INavigationService _navigationService;
        #endregion

        #region CONSTRUCTOR
        public ReminderCreateViewModel(IReminderService service, IReminderAttachmentService attachmentService, INavigationService navigationService)
        {
            _service = service;
            _attachmentService = attachmentService;
            _navigationService = navigationService;
        }
        #endregion

        #region OBSERVABLE PROPERTIES        
        [ObservableProperty]
        private string _title = string.Empty;
        [ObservableProperty]
        private string _description = string.Empty;
        [ObservableProperty]
        private DateTime _selectedDate = DateTime.Now;
        [ObservableProperty]
        private TimeSpan _selectedTime;
        [ObservableProperty]
        private string _attachmentFileName = string.Empty;
        #endregion

        #region RELAY COMMANDS
        [RelayCommand]
        private async Task ReturnAsync() => await _navigationService.NavigateToRootAsync((int)AppShellType.HomePage);
        [RelayCommand]
        private async Task RegisterReminderAsync()
        {
            try
            {
                IsBusy = true;

                if (!string.IsNullOrEmpty(_title))
                {
                    Reminder reminder = new()
                    {
                        Title = _title,
                        Description = _description,
                        ExpirationDate = _selectedDate.Date + SelectedTime,
                        CreationDate = DateTime.Now
                    };

                    await _service.InitializeAsync();
                    await _service.CreateItemAsync(reminder);

                    // Attach Images
                    if (!string.IsNullOrWhiteSpace(AttachmentFileName))
                        await ReminderAttachImagesAsync(reminder);

                    await Toast.Make($"Lembrete criado com sucesso!", ToastDuration.Long).Show();
                    await _navigationService.NavigateToRootAsync((int)AppShellType.HomePage);
                }
                else
                {
                    await Toast.Make($"Por favor, informar um título!", ToastDuration.Long).Show();
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"Atenção! Não foi possível registrar o lembrete!\n {ex.Message}", ToastDuration.Long).Show();
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
            Title = string.Empty;
            Description = string.Empty;
            AttachmentFileName = string.Empty;
            SelectedDate = DateTime.Now;
            SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }
        #endregion

        #region PRIVATE METHODS
        private async Task ReminderAttachImagesAsync(Reminder reminder)
        {
            // Register attachments
            var reminderAttach = new ReminderAttachment
            {
                ReminderId = reminder.ReminderId,
                AttachmentFileName = AttachmentFileName,
                CreationDate = DateTime.Now
            };
            await _attachmentService.InitializeAsync();
            await _attachmentService.CreateItemAsync(reminderAttach);
        }
        #endregion
    }
}
