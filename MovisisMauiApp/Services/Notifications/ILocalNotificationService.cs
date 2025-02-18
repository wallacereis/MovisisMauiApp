namespace MovisisMauiApp.Services.Notifications
{
    public interface ILocalNotificationService
    {
        Task<bool> SendLocalNotification(Reminder reminder);
    }
}
