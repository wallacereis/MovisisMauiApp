namespace MovisisMauiApp.Services.Database
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected SQLiteAsyncConnection _dbConnection;      

        public async Task InitializeAsync()
        {
            await SetUpDbAsync();
        }

        private async Task SetUpDbAsync()
        {
            if (_dbConnection is not null)
                return;

            _dbConnection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _dbConnection.CreateTableAsync<Reminder>();
            await _dbConnection.CreateTableAsync<ReminderAttachment>();
        }

        public async Task<int> CreateItemAsync(T entity) => await _dbConnection.InsertAsync(entity);

        public async Task<int> UpdateItemAsync(T entity) => await _dbConnection.UpdateAsync(entity);

        public async Task<int> DeleteItemAsync(T entity) => await _dbConnection.DeleteAsync(entity);

        public void Dispose() => _dbConnection?.GetConnection().Dispose();
    }
}
