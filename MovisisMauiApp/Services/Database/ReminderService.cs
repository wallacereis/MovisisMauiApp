namespace MovisisMauiApp.Services.Database
{
    public class ReminderService : BaseService<Reminder>, IReminderService
    {
        public async Task<Reminder> GetByIdAsync(int id)
        {
            var item = await _dbConnection
                .Table<Reminder>()
                .FirstOrDefaultAsync(x => x.ReminderId == id);
            return item;
        }

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            // Using QueryAsync for a more custom query
            var query = "SELECT * FROM Reminder p1 LEFT JOIN ReminderAttachment p2 ON p1.ReminderId = p2.ReminderId ";
            var reminders = await _dbConnection.QueryAsync<Reminder>(query);

            var resultList = (await Task.WhenAll(reminders
                .GroupBy(x => x.ReminderId)
                .SelectMany(g => g)
                .Select(async g => new Reminder
                {
                    ReminderId = g.ReminderId,
                    Title = g.Title,
                    Description = g.Description,
                    ExpirationDate = g.ExpirationDate,
                    CreationDate = g.CreationDate,
                    ReminderAttachments = await GetReminderAttachmentsAsync(g.ReminderId)
                })
                .ToList()
            ))
            .OrderBy(x => x.ExpirationDate)
            .ToList();

            return resultList;
        }

        private async Task<List<ReminderAttachment>> GetReminderAttachmentsAsync(int reminderId)
        {
            var items = await _dbConnection
                .Table<ReminderAttachment>()
                .Where(x => x.ReminderId == reminderId)
                .OrderBy(x => x.CreationDate)
                .ToListAsync();
            return items;
        }

        public async Task<IEnumerable<Reminder>> GetAllByExpirationDateAsync(DateTime? expirationDate)
        {
            // Using QueryAsync for a more custom query
            var query = "SELECT * FROM Reminder p1 LEFT JOIN ReminderAttachment p2 ON p1.ReminderId = p2.ReminderId ";
            var reminders = await _dbConnection.QueryAsync<Reminder>(query);

            var resultList = (await Task.WhenAll(reminders
                .Where(x => x.ExpirationDate >= expirationDate)
                .GroupBy(x => x.ReminderId)
                .SelectMany(g => g)
                .Select(async g => new Reminder
                {
                    ReminderId = g.ReminderId,
                    Title = g.Title,
                    Description = g.Description,
                    ExpirationDate = g.ExpirationDate,
                    CreationDate = g.CreationDate,
                    ReminderAttachments = await GetReminderAttachmentsAsync(g.ReminderId)
                })
                .ToList()
            ))
            .OrderBy(x => x.ExpirationDate)
            .ToList();

            return resultList;
        }
    }
}
