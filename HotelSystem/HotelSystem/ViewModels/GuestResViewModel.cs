namespace HotelSystem.ViewModels
{
    public class GuestResViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateOnly CheckIn_Date { get; set; }
        public DateOnly Checkout_Date { get; set; }
    }
}
