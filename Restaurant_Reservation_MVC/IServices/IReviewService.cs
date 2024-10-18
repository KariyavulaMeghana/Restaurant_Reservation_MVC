using Restaurant_Reservation_MVC.Models;

namespace Restaurant_Reservation_MVC.IServices
{
    public interface IReviewService
    {
        Task<string> AddReview(Review reviewCreate);
        Task<string> UpdateReview(Review updatereview);
        Task<string> DeleteReview(int id);
        Task<IEnumerable<Review>> GetAllReviews();
        Task<Review> GetReviewById(int id);
        Task<IEnumerable<Review>> GetReviewsByRestaurantId(int restaurantId);
        Task<IEnumerable<Review>> GetReviewsByUserId(int userId);
    }
}
