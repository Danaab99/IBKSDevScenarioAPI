﻿using IBKSDevScenarioAPI.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IBKSDevScenarioAPI.Models;
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
        public IActionResult GetAllLevels()
        {
            try
            {
                var levels = _context.StatusLevel.ToList();
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
        public IActionResult GetStatusLevel(int id)
        {
            try
            {
                var level = _context.StatusLevel.Find(id);
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
        public IActionResult PostStatusLevel(StatusLevel level)
        {
            try
            {
                _context.StatusLevel.Add(level); // Ensure 'level' does not have an Id set explicitly
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetStatusLevel), new { id = level.Id }, level);
            
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStatusLevel(int id, [FromBody] StatusLevel updatedLevel)
        {
            if (id != updatedLevel.Id)
            {
                return BadRequest("ID mismatch in the URL and the body.");
            }

            var level = _context.StatusLevel.Find(id);
            if (level == null)
            {
                return NotFound($"Status Level with ID {id} not found.");
            }

            try
            {
                level.StatusName = updatedLevel.StatusName;  // Assuming StatusName is the only field to update
                _context.StatusLevel.Update(level);
                _context.SaveChanges();
                return Ok("Status Level updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating status level: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStatusLevel(int id)
        {
            var level = _context.StatusLevel.Find(id);
            if (level == null)
            {
                return NotFound($"Status Level with ID {id} not found.");
            }

            try
            {
                _context.StatusLevel.Remove(level);
                _context.SaveChanges();
                return Ok($"Status Level with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting status level: {ex.Message}");
            }
        }


    }
}
