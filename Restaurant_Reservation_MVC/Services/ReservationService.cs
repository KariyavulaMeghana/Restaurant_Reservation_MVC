using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_MVC.IServices;
using Restaurant_Reservation_MVC.Models;
using System.Text.Json;
using System.Text;

namespace Restaurant_Reservation_MVC.Services
{
      public class ReservationService : IReservationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string _apiUrl;

        public ReservationService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _apiUrl = _config["ApiSettings:AuthURL"];
        }

        public async Task<string> BookReservationAsync(Reservation reservation)
        {
            var reservationJson = new StringContent(JsonSerializer.Serialize(reservation), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, reservationJson);
            response.EnsureSuccessStatusCode();
            return "Reservation booked successfully";
        }

        public async Task<string> UpdateReservationAsync(int id, Reservation updatedReservation)
        {
            var reservationJson = new StringContent(JsonSerializer.Serialize(updatedReservation), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUrl}{id}/update", reservationJson);
            response.EnsureSuccessStatusCode();
            return "Reservation updated successfully";
        }

        public async Task<string> CancelReservationAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}{id}");
            response.EnsureSuccessStatusCode();
            return "Reservation canceled successfully";
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Reservation>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        //public async Task<List<Reservation>> GetReservationsAsync()
        //{
        //    var response = await _httpClient.GetAsync(_apiUrl);
        //    response.EnsureSuccessStatusCode();

        //    var jsonString = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<List<Reservation>>(jsonString);
        //}


        //public async Task<Reservation?> GetReservationByIdAsync(int id)
        //{
        //    var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
        //    response.EnsureSuccessStatusCode();
        //    var responseStream = await response.Content.ReadAsStreamAsync();
        //    return await JsonSerializer.DeserializeAsync<Reservation>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //}
        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Reservation>(jsonString);
        }


        public async Task<List<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}user/{userId}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Reservation>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<Reservation>> GetReservationsByRestaurantIdAsync(int restaurantId)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}restaurant/{restaurantId}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Reservation>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<Reservation>> GetReservationsByDateAsync(DateTime date)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}date/{date:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Reservation>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }


    }
}
