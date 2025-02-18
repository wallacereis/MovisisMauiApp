using Flurl;
using Flurl.Http;

namespace MovisisMauiApp.Services.Http
{
    public class FlurlAPI<T> : IFlurlAPI<T> where T : class
    {
        public async Task<T> GetItemByIdAsync(string entity, int id, string token)
        {
            return await Constants.BaseAddressAPI
                .AppendPathSegment($"{entity}/{id}")
                .WithHeader("Accept", "application/json")
                .WithOAuthBearerToken(token)
                .AllowHttpStatus("400-500")
                .WithTimeout(TimeSpan.FromMinutes(2))
                .GetJsonAsync<T>();
        }

        public async Task<T> GetItemByEmailAsync(string entity, string email, string token)
        {
            return await Constants.BaseAddressAPI
                .AppendPathSegment($"{entity}/{email}")
                .WithHeader("Accept", "application/json")
                .WithOAuthBearerToken(token)
                .AllowHttpStatus("400-500")
                .WithTimeout(TimeSpan.FromMinutes(2))
                .GetJsonAsync<T>();
        }

        public async Task<T> GetItemByParameterAsync(string entity, string parameter, int id, string document, string token)
        {
            return await Constants.BaseAddressAPI
                .AppendPathSegment($"{entity}/{parameter}/{id}/{document}")
                .WithHeader("Accept", "application/json")
                .WithOAuthBearerToken(token)
                .AllowHttpStatus("400-500")
                .WithTimeout(TimeSpan.FromMinutes(2))
                .GetJsonAsync<T>();
        }

        public async Task<T> GetItemByLoginAsync(string entity, string emailParameter, string passwordParameter)
        {
            return await Constants.BaseAddressAPI
                .AppendPathSegment($"{entity}")
                .SetQueryParams
                (
                    new
                    {
                        email = emailParameter,
                        username = passwordParameter
                    }
                )
                .WithHeader("Accept", "application/json")
                .AllowHttpStatus("400-500")
                .WithTimeout(TimeSpan.FromMinutes(2))
                .GetJsonAsync<T>();
        }

        public async Task<T> GetItemByDocumentAsync(string entity, string parameter, string document)
        {
            return await Constants.BaseAddressAPI
                .AppendPathSegment($"{entity}/{parameter}/{document}")
                .WithHeader("Accept", "application/json")
                //.WithOAuthBearerToken(token)
                .AllowHttpStatus("400-500")
                .WithTimeout(TimeSpan.FromMinutes(2))
                .GetJsonAsync<T>();
        }

        public async Task<List<T>> GetAllItemsAsync(string entity, string parameter, string value)
        {
            return await Constants.BaseAddressAPI
                .AppendPathSegment($"{entity}/{parameter}/{value}")
                .WithHeader("Accept", "application/json")
                .AllowHttpStatus("400-500")
                .WithTimeout(TimeSpan.FromMinutes(2))
                .GetJsonAsync<List<T>>();
        }

        public async Task<List<T>> GetAllItemsByIdAsync(string entity, int id, string token)
        {
            return await Constants.BaseAddressAPI
                .AppendPathSegment($"{entity}/{id}")
                .WithHeader("Accept", "application/json")
                .WithOAuthBearerToken(token)
                .AllowHttpStatus("400-500")
                .WithTimeout(TimeSpan.FromMinutes(2))
                .GetJsonAsync<List<T>>();
        }

        public async Task<T> AddItemAsync(string entity, T obj, string token)
        {
            var result = await Constants.BaseAddressAPI
                .AppendPathSegment($"{entity}")
                .WithHeader("Accept", "application/json")
                .WithOAuthBearerToken(token)
                .AllowHttpStatus("400-500")
                .WithTimeout(TimeSpan.FromMinutes(2))
                .PostJsonAsync(obj);
            //.ReceiveJson<T>();
            return await result.GetJsonAsync<T>();
        }

        public async Task<bool> UpdateItemAsync(string entity, int id, T obj, string token)
        {
            var result = await Constants.BaseAddressAPI
                .AppendPathSegment($"{entity}/{id}")
                .WithHeader("Accept", "application/json")
                .WithOAuthBearerToken(token)
                .AllowHttpStatus("400-500")
                .WithTimeout(TimeSpan.FromMinutes(2))
                .PutJsonAsync(obj);
            //.ReceiveJson<T>();
            return result.ResponseMessage.IsSuccessStatusCode;
        }

        public async Task<T> DeleteItemAsync(string entity, int id, string token)
        {
            return await Constants.BaseAddressAPI
                .AppendPathSegment($"{entity}/{id}")
                .WithHeader("Accept", "application/json")
                .WithOAuthBearerToken(token)
                .AllowHttpStatus("400-500")
                .WithTimeout(TimeSpan.FromMinutes(2))
                .DeleteAsync()
                .ReceiveJson<T>();
        }

        public Task<byte[]> GetFileAsync(string file, string token)
        {
            //TODO
            throw new NotImplementedException();
        }

        public async Task<T> UploadItemAsync(string entity, int id, string token)
        {
            return await Constants.BaseAddressAPI
                .AppendPathSegment($"{entity}/{id}")
                .WithHeader("Accept", "application/json")
                .WithOAuthBearerToken(token)
                .AllowHttpStatus("400-500")
                .WithTimeout(TimeSpan.FromMinutes(2))
                .PostJsonAsync(id)
                .ReceiveJson<T>();
        }

        public async Task<bool> UploadItemAsync(string entity, Stream file, int id, string token)
        {
            var result = await Constants.BaseAddressAPI
                .AppendPathSegment($"{entity}/{id}")
                //.WithHeader("Accept", "application/json")
                .WithHeader("Content-Type", "multipart/form-data")
                .WithOAuthBearerToken(token)
                .AllowHttpStatus("400-500")
                .WithTimeout(TimeSpan.FromMinutes(2))
                .PostJsonAsync(file);
                //.ReceiveJson<T>();
            return result.ResponseMessage.IsSuccessStatusCode;
        }
    }
}
