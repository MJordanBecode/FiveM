using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; // Pense à installer le package NuGet "Newtonsoft.Json" si ce n'est pas fait

namespace Lostgen.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "http://localhost:5000/api/"; // Remplace par l'URL de ton API

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseUrl),
                Timeout = TimeSpan.FromSeconds(10) // Évite de bloquer indéfiniment si l'API crash
            };

            // Ici tu pourras ajouter ta clé API dans les Headers pour sécuriser la communication
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", "TaCleSecuriseePrevue");
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, content);
                if (!response.IsSuccessStatusCode) return default;

                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(responseString);
            }
            catch (Exception ex)
            {
                CitizenFX.Core.Debug.WriteLine($"[API Error] Impossible de joindre l'API : {ex.Message}");
                return default;
            }
        }

        public async Task<TResponse> GetAsync<TResponse>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode) return default;

                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(responseString);
            }
            catch (Exception ex)
            {
                CitizenFX.Core.Debug.WriteLine($"[API Error] {ex.Message}");
                return default;
            }
        }
    }
}