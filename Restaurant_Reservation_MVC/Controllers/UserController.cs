using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_MVC.IServices;
using Restaurant_Reservation_MVC.Models;

namespace Restaurant_Reservation_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userServices.GetAllUsers();
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userServices.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: User/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.Login(model);
                if (result == "Login successful")
                {
                    //return RedirectToAction("Index");
                    TempData["SuccessMessage"] = "Login successful!";
                    return View("Login");
                }
                ModelState.AddModelError("", result);
            }
            return View(model);
        }


        // GET: User/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userServices.RegisterNewUser(user);
                    if (result == "User added successfully")
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", result);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(user);
        }
    }
}
