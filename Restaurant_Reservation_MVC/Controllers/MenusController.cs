using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant_Reservation_MVC.DTO;
using System.Text;

namespace Restaurant_Reservation_MVC.Controllers
{
    public class MenusController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public MenusController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiSettings:MenuURL"];
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var menus = JsonConvert.DeserializeObject<List<MenuDTO>>(jsonData);
                return View(menus);
            }
            return View(new List<MenuDTO>());

        }
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var menu = JsonConvert.DeserializeObject<MenuDTO>(jsonData);
                return View(menu);
            }
            return NotFound();
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            return View();

        }
        // POST: Menus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuDTO menuDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(menuDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(menuDto);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var menu = JsonConvert.DeserializeObject<MenuDTO>(jsonData);
                return View(menu);
            }
            return NotFound();

        }
        // POST: Menus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuDTO menuDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(menuDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_apiUrl}{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(menuDto);

        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var menu = JsonConvert.DeserializeObject<MenuDTO>(jsonData);
                return View(menu);
            }
            return NotFound();
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetMenusByRestaurantId(int restaurantId)
        {
            try
            {
                // Make an HTTP GET request to the API endpoint to get menus by restaurant ID
                var response = await _httpClient.GetAsync($"{_apiUrl}{restaurantId}");

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    if (jsonData.TrimStart().StartsWith("["))
                    {
                        // If the JSON is an array, deserialize as List<MenuDTO>
                        var menus = JsonConvert.DeserializeObject<List<MenuDTO>>(jsonData);
                        return View(menus ?? new List<MenuDTO>());
                    }
                    else
                    {
                        // If the JSON is a single object, deserialize as MenuDTO and put it into a list
                        var menu = JsonConvert.DeserializeObject<MenuDTO>(jsonData);
                        return View(menu != null ? new List<MenuDTO> { menu } : new List<MenuDTO>());
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to load menus.";
                    return View(new List<MenuDTO>());
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return View(new List<MenuDTO>());
            }
        }

    }
}
