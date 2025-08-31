using BookingAPI.Models;
using BookingAPI.Services;

namespace BookingApi.Services
{
    public class HomeService : IHomeService
    {
        private readonly Dictionary<string, (string HomeName, List<string> Slots)> _homes =
            new()
            {
                { "123", ("Home 1", new List<string> { "2025-07-15", "2025-07-16" }) },
                { "456", ("Home 2", new List<string> { "2025-07-17", "2025-07-18" }) }
            };

        public async Task<List<Home>> GetAvailableHomesAsync(DateTime startDate, DateTime endDate)
        {
            var requestedDates = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                           .Select(offset => startDate.AddDays(offset))
                                           .ToHashSet();

            return await Task.Run(() =>
            {
                return _homes
                    .Where(h => requestedDates.All(date => h.Value.Slots.Contains(date.ToString("yyyy-MM-dd"))))
                    .Select(h => new Home
                    {
                        HomeId = h.Key,
                        HomeName = h.Value.HomeName,
                        AvailableSlots = h.Value.Slots
                    })
                    .ToList();
            });
        }
    }
}
