namespace MovisisMauiApp.Services.Database
{
    public class ReminderAttachmentService : BaseService<ReminderAttachment>, IReminderAttachmentService
    {
        public async Task<IEnumerable<ReminderAttachment>> GetAllByReminderIdAsync(int reminderId)
        {
            var items = await _dbConnection
                .Table<ReminderAttachment>()
                .Where(x => x.ReminderId == reminderId)
                .OrderBy(x => x.CreationDate)
                .ToListAsync();

            return items;
        }
    }
}
