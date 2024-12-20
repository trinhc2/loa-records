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

    // GET: api/EncounterPreview/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EncounterPreview>> GetEncounterPreview(int id)
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
}
