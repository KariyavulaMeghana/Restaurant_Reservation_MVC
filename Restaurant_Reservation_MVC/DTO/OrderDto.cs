namespace Restaurant_Reservation_MVC.DTO
{
    public class OrderDto
    {
        public int OrderId { get; internal set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
    }
}
