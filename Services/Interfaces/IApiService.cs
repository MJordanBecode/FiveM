using System.Threading.Tasks;

namespace Lostgen.Services
{
    public interface IApiService
    {
        Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<TResponse> GetAsync<TResponse>(string endpoint);
    }
}