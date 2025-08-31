namespace BookingAPI.Models
{
    public class AvailableHomesResponse
    {
        public string Status { get; set; } = "OK";
        public List<Home> Homes { get; set; } = new();
    }
}
