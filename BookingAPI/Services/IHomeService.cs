using BookingAPI.Models;

namespace BookingAPI.Services
{
    public interface IHomeService
    {
        Task<List<Home>> GetAvailableHomesAsync(DateTime startDate, DateTime endDate);
    }
}
