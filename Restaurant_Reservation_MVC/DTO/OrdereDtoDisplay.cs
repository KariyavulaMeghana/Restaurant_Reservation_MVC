namespace Restaurant_Reservation_MVC.DTO
{
    public class OrdereDtoDisplay
    {
        public int OrderId
        {
            get; set;
        }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}

