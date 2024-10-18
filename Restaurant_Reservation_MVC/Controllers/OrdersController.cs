using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant_Reservation_MVC.DTO;
using System.Text;

namespace Restaurant_Reservation_MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public OrdersController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiSettings:OrderURL"];
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<OrdereDtoDisplay>>(jsonData);
                return View(orders);
            }
            return View(new List<OrderDto>());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<OrdereDtoDisplay>(jsonData);
                return View(order);
            }
            return NotFound();
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(orderDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "Order created successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(orderDto);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<OrderDto>(jsonData);
                return View(order);
            }
            return NotFound();
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(orderDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_apiUrl}{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "Order updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(orderDto);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<OrdereDtoDisplay>(jsonData);
                return View(order);
            }
            return NotFound();
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["AlertMessage"] = "Order deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
