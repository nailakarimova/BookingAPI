About The Project

***Booking API 
Booking API is a lightweight ASP.NET Core Web API that returns a list of homes available for a given date range.
All data is stored in memory, no database required.


***How to Run the Application

Clone the repository:
git clone https://github.com/yourusername/BookingApi.git
cd BookingApi

Run the API:
dotnet run --project BookingAPI

Open Swagger UI in your browser:
https://localhost:5001/swagger

or

http://localhost:5000/swagger


***How to Test the Application

Navigate to the test project:
cd BookingApi.Tests

Run tests:
dotnet test

What is tested:
- Homes are returned if all requested dates are available.
- Empty result if no homes match.
- Input validation: if endDate < startDate, the API returns 400 Bad Request.

Tests use xUnit + Microsoft.AspNetCore.Mvc.Testing + FluentAssertions for full integration testing.


***Architecture

1. Controllers
   Handle HTTP requests, no business logic.
   1.1. AvailableHomesController exposes GET /api/available-homes.

2. Services
   Contain business logic.
   2.1. IHomeService interface.
   2.2. HomeService implements filtering and stores in-memory data.

3. Models
   Represent data and DTOs.
   3.1. Home
   3.2. AvailableHomesResponse

4. In-Memory Storage
   4.1. Homes stored in a Dictionary<string, (string HomeName, HashSet<DateTime> Slots)>.
   4.2. HashSet chosen for O(1) slot lookups (faster than List for large datasets).


***Filtering Logic
1. User supplies a startDate and endDate.
2. Service generates the full date range (startDate..endDate).
3. A home is included only if all dates in the range exist in its slots.
4. Implemented efficiently using HashSet<DateTime> lookups.
5. Filtering runs inside Task.Run → keeps the API responsive by not blocking request threads.


***Summary
• Single endpoint: GET /api/available-homes
• Fully in-memory (no DB).
• Async filtering with Task.Run.
• Optimized with HashSet for large datasets.
• Clean layered architecture (Controller → Service → Model).
• Fully integration tested.



