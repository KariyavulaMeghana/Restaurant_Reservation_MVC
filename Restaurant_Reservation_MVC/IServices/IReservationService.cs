using Restaurant_Reservation_MVC.Models;

namespace Restaurant_Reservation_MVC.IServices
{
    public interface IReservationService
    {
        Task<string> BookReservationAsync(Reservation reservation);
        Task<string> UpdateReservationAsync(int id, Reservation updatedReservation);
        Task<string> CancelReservationAsync(int id);
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<Reservation?> GetReservationByIdAsync(int id);
        Task<List<Reservation>> GetReservationsByUserIdAsync(int userId);
        Task<List<Reservation>> GetReservationsByRestaurantIdAsync(int restaurantId);
        Task<List<Reservation>> GetReservationsByDateAsync(DateTime date);
    }
}
