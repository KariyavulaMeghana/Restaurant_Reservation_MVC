using Restaurant_Reservation_MVC.DTO;
using Restaurant_Reservation_MVC.Models;
using System.Text.Json;
using System.Text;
using Restaurant_Reservation_MVC.IServices;
using NuGet.Protocol.Plugins;

namespace Restaurant_Reservation_MVC.Services
{
    public class UserServices : IUserServices
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

        public async Task<User> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"{_configuration["ApiSettings:AuthURLUser"]}/{id}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<User>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

       
        public async Task<LoginResponse> Login(LoginModel model)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["ApiSettings:AuthURLUser"]}/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseData); // Log the response

                try
                {
                    var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return loginResponse;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Deserialization error: {ex.Message}");
                }
            }

            return new LoginResponse
            {
                Message = "Invalid username or password",
                Role = null
            };
        }
        public async Task<string> RegisterNewUser(User user)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["ApiSettings:AuthURLUser"]}/register", jsonContent);

            return response.IsSuccessStatusCode ? "User added successfully" : "Failed to add user";
        }

        public async Task<string> UpdateUser(int id, User user)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_configuration["ApiSettings:AuthURLUser"]}/{id}", jsonContent);

            return response.IsSuccessStatusCode ? "User updated successfully" : "Failed to update user";
        }

        public async Task<string> DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_configuration["ApiSettings:AuthURLUser"]}/{id}");

            return response.IsSuccessStatusCode ? "User deleted successfully" : "Failed to delete user";
        }
    }
}