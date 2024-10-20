using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant_Reservation_MVC.DTO;
using Restaurant_Reservation_MVC.IServices;
using Restaurant_Reservation_MVC.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace User_Management_MVC.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IRestaurantServices _restaurantService;
        private readonly IReviewService _reviewService;
        private readonly IReservationService _reservationService;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public DashBoardController(HttpClient httpClient, IConfiguration configuration, IUserServices userServices, IRestaurantServices restaurantServices, IReservationService reservationService, IReviewService reviewService)
        {
            _httpClient = httpClient;
            _userServices = userServices;
            _restaurantService = restaurantServices;
            _reservationService = reservationService;
            _reviewService = reviewService;
            _apiUrl = configuration["ApiSettings:MenuItemURL"];  // Load API URL from configuration
        }

        // GET: Dashboard/AdminDashboard
        public async Task<IActionResult> AdminDashboard()
        {
            var model = new AdminDashBoard();
            try
            {
                model.UserDTOs = await _userServices.GetAllUsers();
                model.Restaurants = await _restaurantService.GetAllRestaurants();

                
                model.Reviews = (List<Review>)await _reviewService.GetAllReviews();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the dashboard: " + ex.Message;
            }

            return View(model);
        }

        
    }
}