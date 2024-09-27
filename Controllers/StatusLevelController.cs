using IBKSDevScenarioAPI.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IBKSDevScenarioAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace IBKSDevScenarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusLevelController : ControllerBase
    {
        private readonly DevScenarioDbContext _context;

        public StatusLevelController(DevScenarioDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLevels()
        {
            try
            {
                var levels = await _context.StatusLevel.ToListAsync();
                if (levels.Count == 0)
                {
                    return NotFound("No Status Level found.");
                }
                return Ok(levels);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusLevel(int id)
        {
            try
            {
                var level = await _context.StatusLevel.FindAsync(id);
                if (level == null)
                {
                    return NotFound("Application no found.");

                }
                return Ok(level);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostStatusLevel(StatusLevel level)
        {
            try
            {
                _context.StatusLevel.Add(level); // Ensure 'level' does not have an Id set explicitly
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetStatusLevel), new { id = level.Id }, level);
            
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatusLevel(int id, [FromBody] StatusLevel updatedLevel)
        {
            if (id != updatedLevel.Id)
            {
                return BadRequest("ID mismatch in the URL and the body.");
            }

            var level = await _context.StatusLevel.FindAsync(id);
            if (level == null)
            {
                return NotFound($"Status Level with ID {id} not found.");
            }

            try
            {
                level.StatusName = updatedLevel.StatusName;  // Assuming StatusName is the only field to update
                _context.StatusLevel.Update(level);
                await _context.SaveChangesAsync();
                return Ok("Status Level updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating status level: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusLevel(int id)
        {
            var level = await _context.StatusLevel.FindAsync(id);
            if (level == null)
            {
                return NotFound($"Status Level with ID {id} not found.");
            }

            try
            {
                _context.StatusLevel.Remove(level);
                await _context.SaveChangesAsync();
                return Ok($"Status Level with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting status level: {ex.Message}");
            }
        }


    }
}
