using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant_Reservation_MVC.DTO;
using System.Text;

namespace Restaurant_Reservation_MVC.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public MenuItemsController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiSettings:MenuItemURL"];
        }

        // GET: MenuItems
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var menuItems = JsonConvert.DeserializeObject<List<MenuItemDTO>>(jsonData);
                return View(menuItems);
            }
            return View(new List<MenuItemDTO>());

        }
        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var menuItem = JsonConvert.DeserializeObject<MenuItemDTO>(jsonData);
                return View(menuItem);
            }
            return NotFound();
        }

        // GET: MenuItems/Create
        public IActionResult Create()
        {
            return View();

        }
        // POST: MenuItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItemDTO menuItemDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(menuItemDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(menuItemDto);
        }

        // GET: MenuItems/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var menuItem = JsonConvert.DeserializeObject<MenuItemDTO>(jsonData);
                return View(menuItem);
            }
            return NotFound();

        }
        // POST: MenuItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuItemDTO menuItemDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(menuItemDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_apiUrl}{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(menuItemDto);
        }

        // GET: MenuItems/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var menuItem = JsonConvert.DeserializeObject<MenuItemDTO>(jsonData);
                return View(menuItem);
            }
            return NotFound();

        }
        // POST: MenuItems/Delete/5
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
    }
}
