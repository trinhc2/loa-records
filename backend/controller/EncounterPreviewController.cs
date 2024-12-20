using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using database.Models;

[Route("api/[controller]")]
[ApiController]
public class EncounterPreviewController : ControllerBase
{
    private readonly EncounterPreviewContext _context;

    public EncounterPreviewController(EncounterPreviewContext context)
    {
        _context = context;
    }

    // GET: api/EncounterPreview
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EncounterPreview>>> GetEncounterPreviews()
    {
        try
        {
            var encounterPreviews = await _context.EncounterPreviews.ToListAsync();
            return Ok(encounterPreviews); // Return list of EncounterPreviews
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message); // Handle errors
        }
    }

    // GET: api/EncounterPreview/id/5
    [HttpGet("id/{id}")]
    public async Task<ActionResult<EncounterPreview>> GetEncounterPreviewById(int id)
    {
        try
        {
            var encounterPreview = await _context.EncounterPreviews.FindAsync(id);
            if (encounterPreview == null)
            {
                return NotFound(); // If no encounter found with the given id
            }
            return Ok(encounterPreview); // Return a single EncounterPreview
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // GET: api/EncounterPreview/name/someName
    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<EncounterPreview>>> GetPlayerEncounters(string localPlayer)
    {
        try
        {
            // Using LINQ with Where() to filter by name
            var encounterPreviews = await _context.EncounterPreviews
                .Where(e => e.local_player == localPlayer && e.cleared == true)
                .ToListAsync(); // Execute the query asynchronously

            if (encounterPreviews == null || encounterPreviews.Count == 0)
            {
                return NotFound(); // Return 404 if no encounter previews are found
            }

            return Ok(encounterPreviews); // Return the list of matching encounter previews
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message); // Handle errors
        }
    }
    // GET: api/EncounterPreview?localPlayer=JohnDoe&currentBoss=Dragon
    [HttpGet("boss")]
    public async Task<ActionResult<IEnumerable<EncounterPreview>>> GetBoss([FromQuery] string localPlayer, [FromQuery] string currentBoss)
    {
        try
        {
            // Start with the base query
            var query = _context.EncounterPreviews.AsQueryable();

            // Filter by local_player if it's provided
            if (!string.IsNullOrEmpty(localPlayer))
            {
                query = query.Where(e => e.local_player == localPlayer);
            }

            // Filter by current_boss if it's provided
            if (!string.IsNullOrEmpty(currentBoss))
            {
                query = query.Where(e => e.current_boss == currentBoss);
            }

            // Filter by cleared
            query = query.Where(e => e.cleared == true);

            // Execute the query asynchronously
            var encounterPreviews = await query.ToListAsync();

            if (encounterPreviews == null || encounterPreviews.Count == 0)
            {
                return NotFound(); // Return 404 if no matching records
            }

            return Ok(encounterPreviews); // Return the matching records
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message); // Handle errors
        }
    }
}