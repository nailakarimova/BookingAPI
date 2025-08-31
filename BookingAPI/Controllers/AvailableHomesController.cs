using BookingAPI.Models;
using BookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailableHomesController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public AvailableHomesController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        public async Task<ActionResult<AvailableHomesResponse>> Get([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (endDate < startDate)
                return BadRequest("End date must be after start date.");

            var homes = await _homeService.GetAvailableHomesAsync(startDate, endDate);

            return Ok(new AvailableHomesResponse { Homes = homes });
        }
    }
}
