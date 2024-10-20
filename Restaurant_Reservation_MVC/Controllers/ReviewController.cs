using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Restaurant_Reservation_MVC.IServices;
using Restaurant_Reservation_MVC.Models;

namespace Restaurant_Reservation_MVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var reviews = await _reviewService.GetAllReviews();
                return View(reviews);
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to load reviews.";
                return View(new List<Review>());
            }
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var review = await _reviewService.GetReviewById(id);
                if (review == null)
                {
                    return NotFound();
                }
                return View(review);
            }
            catch (HttpRequestException ex)
            {
                TempData["ErrorMessage"] = $"Failed to load review details. {ex.Message}";
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
        public async Task<IActionResult> Create([Bind("UserID,RestaurantID,Rating,Comment")] Review review)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input.";
                return View(review);
            }

            var result = await _reviewService.AddReview(review);
            //TempData["SuccessMessage"] = result;
            TempData["AlertMessage"] = "Review is added successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var review = await _reviewService.GetReviewById(id);
                if (review == null)
                {
                    return NotFound();
                }
                return View(review);
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to load review for editing.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewID,UserID,RestaurantID,Rating,Comment")] Review review)
        {
            if (id != review.ReviewID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input.";
                return View(review);
            }

            var result = await _reviewService.UpdateReview(review);
            // TempData["SuccessMessage"] = result;
            TempData["AlertMessage"] = "Review edited successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var review = await _reviewService.GetReviewById(id);
                if (review == null)
                {
                    return NotFound();
                }
                return View(review);
            }
            catch
            {
                TempData["ErrorMessage"] = "Failed to load review for deletion.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _reviewService.DeleteReview(id);
            if (result == "Review deleted successfully")
            {
                //TempData["SuccessMessage"] = result;
                TempData["AlertMessage"] = "Review deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Failed to delete review.";
            return RedirectToAction(nameof(Index));
        }
    }
}