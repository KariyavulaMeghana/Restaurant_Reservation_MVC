using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant_Reservation_MVC.DTO;
using System.Text;

namespace Restaurant_Reservation_MVC.Controllers
{
    public class TablesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public TablesController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiSettings:TableURL"];
        }

        // GET: TablesController
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}"+"all");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tables = JsonConvert.DeserializeObject<List<TableDtoDisplay>>(jsonData);
                return View(tables);
            }
            return View(new List<TableDto>());
        }

        // GET: TablesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var table = JsonConvert.DeserializeObject<TableDtoDisplay>(jsonData);
                return View(table);
            }
            return NotFound();
        }

        // GET: TablesController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TablesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TableDto tableDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(tableDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "Table added successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(tableDto);
        }

        // GET: TablesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var table = JsonConvert.DeserializeObject<TableDto>(jsonData);
                return View(table);
            }
            return NotFound();
        }

        // POST: TablesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TableDto tableDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(tableDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_apiUrl}{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "Table edited successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(tableDto);
        }

        // GET: TablesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var table = JsonConvert.DeserializeObject<TableDtoDisplay>(jsonData);
                return View(table);
            }
            return NotFound();
        }

        // POST: TablesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["AlertMessage"] = "Table deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: TablesController/CheckAvailability
        public async Task<IActionResult> CheckAvailability()
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}availability");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var availableTables = JsonConvert.DeserializeObject<List<TableDtoDisplay>>(jsonData);
                TempData["AlertMessage"] = "Table availability checked.";
                return View("CheckAvailability", availableTables);  // Use the name of your view here
            }

            TempData["AlertMessage"] = "Failed to check table availability.";
            return View("CheckAvailability", new List<TableDtoDisplay>());  // Same view
        }

        // GET: TablesController/ByRestaurant/{restaurantId}
        public async Task<IActionResult> GetTablesByRestaurant(int restaurantId)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}restaurant/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tables = JsonConvert.DeserializeObject<List<TableDto>>(jsonData);
                return View(tables);
            }
            return View(new List<TableDto>());
        }
    }
}
