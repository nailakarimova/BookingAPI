namespace BookingAPI.Models
{
    public class Home
    {
        public string HomeId { get; set; } = string.Empty;
        public string HomeName { get; set; } = string.Empty;
        public List<string> AvailableSlots { get; set; } = new();
    }
}
