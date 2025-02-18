namespace MovisisMauiApp.Services.Notifications
{
    public class LocalNotificationService : ILocalNotificationService
    {
        public async Task<bool> SendLocalNotification(Reminder reminder)
        {
            var request = new NotificationRequest
            {
                NotificationId = reminder.ReminderId,
                Title = $"Atenção! Lembrete próximo do vencimento!\nVence em: {reminder.ExpirationDate}",
                Subtitle = reminder.Title,
                Description = reminder.Description,
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5),
                    NotifyRepeatInterval = TimeSpan.FromDays(1),
                }
            };

            return await LocalNotificationCenter.Current.Show(request);
        }
    }
}
