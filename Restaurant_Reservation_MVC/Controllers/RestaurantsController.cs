using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_MVC.IServices;
using Restaurant_Reservation_MVC.Models;

namespace Restaurant_Reservation_MVC.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantServices _restaurantService;

        public RestaurantsController(IRestaurantServices restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var restaurants = await _restaurantService.GetAllRestaurants();
                return View(restaurants);
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to load restaurants.";
                return View(new List<Restaurant>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurantById(id);
                if (restaurant == null)
                {
                    return NotFound();
                }
                return View(restaurant);
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to load restaurant details.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestaurantName,Description,Location,ContactNumber,Rating")] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input.";
                return View(restaurant);
            }

            var result = await _restaurantService.AddNewRestaurant(restaurant);
            //TempData["SuccessMessage"] = result;
            TempData["AlertMessage"] = "Restaurant Added Successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurantById(id);
                if (restaurant == null)
                {
                    return NotFound();
                }
                TempData["AlertMessage"] = "Restaurant Edited Successfully";
                return View(restaurant);

            }
            catch
            {
                //TempData["ErrorMessage"] = "Failed to load restaurant for editing.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RestaurantID,RestaurantName,Description,Location,ContactNumber,Rating")] Restaurant restaurant)
        {
            if (id != restaurant.RestaurantID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input.";
                return View(restaurant);
            }

            var result = await _restaurantService.UpdateRestaurant(restaurant);
            TempData["SuccessMessage"] = result;
            TempData["AlertMessage"] = "Restaurant Edited Successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurantById(id);
                if (restaurant == null)
                {
                    return NotFound();
                }
                return View(restaurant);
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to load restaurant for deletion.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _restaurantService.DeleteRestaurant(id);
            if (result == "Restaurant deleted successfully")
            {
                TempData["SuccessMessage"] = result;
                TempData["AlertMessage"] = "Restaurant Deleted Successfully";

                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Failed to delete restaurant.";

            return RedirectToAction(nameof(Index));
        }
    }
}