namespace MovisisMauiApp.Services.Database
{
    public interface IReminderAttachmentService : IBaseService<ReminderAttachment>
    {
        Task<IEnumerable<ReminderAttachment>> GetAllByReminderIdAsync(int reminderId);
    }
}
