namespace Restaurant_Reservation_MVC.DTO
{
    public class MenuItemDTO
    {
        public int MenuItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int MenuId { get; set; }
    }
}
