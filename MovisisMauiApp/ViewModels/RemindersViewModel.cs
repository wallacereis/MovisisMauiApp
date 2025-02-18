using MovisisMauiApp.Services.Notifications;
namespace MovisisMauiApp.ViewModels
{
    public partial class RemindersViewModel : BaseViewModel
    {
        #region PRIVATE PROPERTIES
        private readonly IReminderService _service;
        private readonly ILocalNotificationService _notificationService;
        private readonly INavigationService _navigationService;        
        #endregion

        #region CONSTRUCTOR
        public RemindersViewModel(IReminderService service, ILocalNotificationService notificationService, INavigationService navigationService)
        {
            _service = service;
            _notificationService = notificationService;
            _navigationService = navigationService;
        }
        #endregion

        #region OBSERVABLE PROPERTIES
        [ObservableProperty]
        private ObservableCollection<Reminder> _reminders;
        [ObservableProperty]
        private DateTime _selectedDate = DateTime.Now;
        #endregion

        #region RELAY COMMANDS
        [RelayCommand]
        private async Task ShowCalendarAsync() => await _navigationService.NavigateToChildPageAsync("RemindersCalendarPage");

        [RelayCommand]
        private async Task ReminderCreateAsync() => await _navigationService.NavigateToChildPageAsync("ReminderCreatePage");

        [RelayCommand]
        private async Task DetailsReminderAsync(Reminder reminder) =>
        await Shell.Current.GoToAsync("ReminderDetailsPage", new Dictionary<string, object>
        {
            {"ReminderObject", reminder }
        });

        [RelayCommand]
        public async Task GetAllRemindersAsync(string? searchBarText)
        {
            try
            {
                IsBusy = true;

                await _service.InitializeAsync();
                var items = await _service.GetAllAsync();

                // Filter by Title or Description
                if(searchBarText is not null)
                {
                    items = items.Where(x =>
                        x.Title.Contains(searchBarText, StringComparison.OrdinalIgnoreCase) ||
                        x.Description.Contains(searchBarText, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }

                if (items.Any())
                {
                    // Load Reminders
                    Reminders = new ObservableCollection<Reminder>(items);

                    // Local Notifications
                    var reminder = items.MinBy(x => x.ExpirationDate);
                    if(reminder?.ExpirationDate.Date <= DateTime.Now.AddDays(1).Date)
                    {
                        var status = await CheckLocalNotificationPermissionsAsync();
                        if(status == PermissionStatus.Granted)
                            await _notificationService.SendLocalNotification(reminder);
                    } 
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"Atenção! Não foi possível obter a lista de lembretes!\n {ex.Message}", ToastDuration.Long).Show();
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task GetRemindersExpirationDateAsync(DateTime? expirationDate)
        {
            try
            {
                IsBusy = true;

                await _service.InitializeAsync();
                var items = await _service.GetAllByExpirationDateAsync(expirationDate);

                if (items.Any())
                {
                    // Load Reminders
                    Reminders = new ObservableCollection<Reminder>(items);

                    // Local Notifications
                    var reminder = items.MinBy(x => x.ExpirationDate);
                    if (reminder?.ExpirationDate.Date <= DateTime.Now.AddDays(1).Date)
                    {
                        var status = await CheckLocalNotificationPermissionsAsync();
                        if (status == PermissionStatus.Granted)
                            await _notificationService.SendLocalNotification(reminder);
                    }
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"Atenção! Não foi possível obter a lista de lembretes!\n {ex.Message}", ToastDuration.Long).Show();
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region PRIVATE METHODS        
        private async Task<PermissionStatus> CheckLocalNotificationPermissionsAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.PostNotifications>();
            }
            return status;
        }
        #endregion
    }
}
