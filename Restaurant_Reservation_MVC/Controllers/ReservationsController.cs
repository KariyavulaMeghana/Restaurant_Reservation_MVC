using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_MVC.IServices;
using Restaurant_Reservation_MVC.Models;

namespace Restaurant_Reservation_MVC.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            try
            {
                var reservations = await _reservationService.GetAllReservationsAsync();
                return View(reservations);
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to load reservations.";
                return View(new List<Reservation>());
            }
        }

        // GET: Reservations/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }
                return View(reservation);
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to load reservation details.";
                return RedirectToAction("Index");
            }
        }

        // GET: Reservations/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RestaurantId,ReservationDate,Status")] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input.";
                return View(reservation);
            }

            var result = await _reservationService.BookReservationAsync(reservation);
            TempData["SuccessMessage"] = result;
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }
                return View(reservation);
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to load reservation for editing.";
                return RedirectToAction("Index");
            }
        }

        // POST: Reservations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,UserId,RestaurantId,ReservationDate,Status")] Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input.";
                return View(reservation);
            }

            var result = await _reservationService.UpdateReservationAsync(id, reservation);
            TempData["SuccessMessage"] = result;
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }
                return View(reservation);
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to load reservation for deletion.";
                return RedirectToAction("Index");
            }
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _reservationService.CancelReservationAsync(id);
            if (result == "Reservation canceled successfully")
            {
                TempData["SuccessMessage"] = result;
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Failed to cancel reservation.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations by User ID
        public async Task<IActionResult> UserReservations(int userId)
        {
            var reservations = await _reservationService.GetReservationsByUserIdAsync(userId);
            return View(reservations);
        }

        // GET: Reservations by Restaurant ID
        public async Task<IActionResult> RestaurantReservations(int restaurantId)
        {
            var reservations = await _reservationService.GetReservationsByRestaurantIdAsync(restaurantId);
            return View(reservations);
        }

        // GET: Reservations by Date
        public async Task<IActionResult> ReservationsByDate(DateTime date)
        {
            var reservations = await _reservationService.GetReservationsByDateAsync(date);
            return View(reservations);
        }
    }
}