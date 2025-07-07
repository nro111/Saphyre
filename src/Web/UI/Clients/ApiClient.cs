using Contracts;
using Shared;
using System.Net.Http.Json;
using System.Text.Json;

namespace UI.Clients
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

        public ApiClient(HttpClient http)
        {
            _http = http;
        }


        public async Task<bool> RegisterAsync(RegistrationDTO registrationDTO)
        {
            var response = await _http.PostAsJsonAsync("api/users/register", registrationDTO, _jsonOptions);
            //response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<bool>(_jsonOptions))!;
        }

        public async Task<bool> LoginAsync(AuthenticationDTO authenticationDTO)
        {
            var response = await _http.PostAsJsonAsync("api/users/login", authenticationDTO, _jsonOptions);
            //response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<bool>(_jsonOptions))!;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var response = await _http.GetAsync("api/users/all");
            //response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<List<UserDTO>>(_jsonOptions))!;
        }

        public async Task<UserDTO?> GetAsync(Guid id)
        {
            var response = await _http.GetAsync($"api/users/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDTO>(_jsonOptions);
        }

        public async Task<bool> CreateAsync(UserDTO user)
        {
            var response = await _http.PostAsJsonAsync("api/users", user, _jsonOptions);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UserDTO user)
        {
            var response = await _http.PutAsJsonAsync($"api/users/{user.Id}", user, _jsonOptions);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/users/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
