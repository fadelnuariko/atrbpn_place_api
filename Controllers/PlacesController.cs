using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlacesAPI.Data;
using PlacesAPI.Models;

namespace PlacesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {
        private readonly PlacesDbContext _context;

        public PlacesController(PlacesDbContext context)
        {
            _context = context;
        }

        // Get all data
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetPlaces()
        {
            return await _context.Places.Where(p => p.IsDeleted == false).ToListAsync();
        }

        // Get single data
        [HttpGet("{id}")]
        public async Task<ActionResult<Place>> ShowPlace(int id)
        {
            var place = await _context.Places.FindAsync(id);

            if (place == null || place.IsDeleted ==true)
            {
                return NotFound(new { message = "Data not found" });
            }

            return place;
        }

        // Save data
        [HttpPost("store")]
        public async Task<ActionResult<Place>> StorePlace(Place place)
        {
            place.Date = DateTime.UtcNow;
            _context.Places.Add(place);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Place added successfully", status="ok" });
        }

        // Update data
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(int id, Place place)
        {
            if (id != place.Id)
            {
                return NotFound(new { message = "Data not found", status="notok" });
            }

            _context.Entry(place).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Places.Any(p => p.Id == id))
                {
                     return NotFound(new {message="Data not found", status="notok"});
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Place updated successfully", status="ok" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var place = await _context.Places.FindAsync(id);

            if (place == null)
            {
                return NotFound(new {message="Data not found"});
            }

            place.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Place deleted successfully", status="ok" });
        }

    }
}
