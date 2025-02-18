namespace MovisisMauiApp.Services.Database
{
    public interface IBaseService<T> : IDisposable where T : class
    {
        Task InitializeAsync();
        Task<int> CreateItemAsync(T entity);
        Task<int> UpdateItemAsync(T entity);
        Task<int> DeleteItemAsync(T entity);        
    }
}
