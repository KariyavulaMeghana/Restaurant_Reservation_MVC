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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _userServices.Login(model);
        //        if (result == "Login successful")
        //        {

        //            TempData["SuccessMessage"] = "Login successful!";
        //            //return View("Index");
        //            return RedirectToAction("Index");
        //        }
        //        ModelState.AddModelError("", result);
        //    }
        //    return View(model);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.Login(model);

                if (result.Message == "Login successful")
                {
                    if (result.Role == "Admin") // Check if role is Owner
                    {
                        //return RedirectToAction("OwnerDashboard", "Dashboard"); // Redirect to Owner Dashboard
                        return View("AdminDashboard");
                    }
                    else if (result.Role == "User")
                    {
                        //return RedirectToAction("AdminDashboard", "Dashboard"); // Redirect to Admin Dashboard
                        return View("UserDashboard");
                    }
                    // Add more roles as needed
                    
                }

                ModelState.AddModelError("", result.Message);
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
                        TempData["SuccessMessage"] = "Registered successful!";
                        //return RedirectToAction("Home/Index");
                        return RedirectToAction("Index", "Home");

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

        //GET: User/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userServices.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.UpdateUser(id, user);
                if (result == "User updated successfully")
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result);
            }
            return View(user);
        }
        // GET: User/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userServices.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _userServices.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}