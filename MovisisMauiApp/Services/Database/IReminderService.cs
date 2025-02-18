namespace MovisisMauiApp.Services.Database
{
    public interface IReminderService : IBaseService<Reminder>
    {
        Task<Reminder> GetByIdAsync(int id);
        Task<IEnumerable<Reminder>> GetAllAsync();
        Task<IEnumerable<Reminder>> GetAllByExpirationDateAsync(DateTime? expirationDate);
    }
}
