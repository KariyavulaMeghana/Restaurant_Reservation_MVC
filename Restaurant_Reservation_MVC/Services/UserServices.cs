using Restaurant_Reservation_MVC.DTO;
using Restaurant_Reservation_MVC.Models;
using System.Text.Json;
using System.Text;
using Restaurant_Reservation_MVC.IServices;

namespace Restaurant_Reservation_MVC.Services
{
    public class UserServices:IUserServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UserServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var response = await _httpClient.GetAsync($"{_configuration["ApiSettings:AuthURLUser"]}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<UserDTO>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"{_configuration["ApiSettings:AuthURLUser"]}/{id}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<UserDTO>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<string> Login(LoginModel model)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["ApiSettings:AuthURLUser"]}/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return "Login successful";
            }
            return "Invalid username or password";
        }

        public async Task<string> RegisterNewUser(User user)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["ApiSettings:AuthURLUser"]}/register", jsonContent);

            return response.IsSuccessStatusCode ? "User added successfully" : "Failed to add user";
        }
    }
}
