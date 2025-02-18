namespace MovisisMauiApp.Services.Http
{
    public interface IFlurlAPI<T> where T : class
    {
        /*-------------------------- Unique Result --------------------------*/
        Task<T> GetItemByIdAsync(string entity, int id, string token);
        Task<T> GetItemByEmailAsync(string entity, string email, string token);
        Task<T> GetItemByParameterAsync(string entity, string parameter, int id, string document, string token);
        Task<T> GetItemByLoginAsync(string entity, string emailParameter, string passwordParameter);
        Task<T> GetItemByDocumentAsync(string entity, string parameter, string document);

        /*-------------------------- List Result --------------------------*/
        Task<List<T>> GetAllItemsAsync(string entity, string parameter, string value);
        Task<List<T>> GetAllItemsByIdAsync(string entity, int id, string token);

        /*-------------------------- CRUD Result --------------------------*/
        Task<T> AddItemAsync(string entity, T obj, string token);
        Task<bool> UpdateItemAsync(string entity, int id, T obj, string token);
        Task<T> DeleteItemAsync(string entity, int id, string token);

        /*-------------------------- File Result --------------------------*/
        Task<byte[]> GetFileAsync(string file, string token);

        /*-------------------------- Upload Result --------------------------*/
        Task<T> UploadItemAsync(string entity, int id, string token);
        Task<bool> UploadItemAsync(string entity, Stream file, int id, string token);
    }
}
