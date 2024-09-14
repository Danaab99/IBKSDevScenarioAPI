using IBKSDevScenarioAPI.DAL;
using IBKSDevScenarioAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IBKSDevScenarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly DevScenarioDbContext _context;

        public ApplicationController(DevScenarioDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllApps() {
            try
            {
                var applications = _context.Application.ToList();
                if (applications.Count == 0)
                {
                    return NotFound("No applications found.");
                }
                return Ok(applications);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetApplication(int id) {
            try
            {
                var application = _context.Application.Find(id);
                if (application == null)
                {
                    return NotFound("Application no found.");

                }
                return Ok(application);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostApplication(Application application) {
            try
            {
                _context.Add(application);
                _context.SaveChanges();
                return Ok("Application created.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateApplication(int id, [FromBody] Application application)
        {
            // Check if the application ID matches the one in the URL
            if (id != application.Id)
            {
                return BadRequest("Application ID mismatch.");
            }

            var existingApplication = _context.Application.Find(id);
            if (existingApplication == null)
            {
                return NotFound("Application not found.");
            }

            // Update the fields of the existing application
            existingApplication.AppStatus = application.AppStatus;
            existingApplication.ProjectRef = application.ProjectRef;
            existingApplication.ProjectName = application.ProjectName;
            existingApplication.ProjectLocation = application.ProjectLocation;
            existingApplication.OpenDt = application.OpenDt;
            existingApplication.StartDt = application.StartDt;
            existingApplication.CompletedDt = application.CompletedDt;
            existingApplication.ProjectValue = application.ProjectValue;
            existingApplication.StatusId = application.StatusId;
            existingApplication.Notes = application.Notes;
            existingApplication.Modified = DateTime.UtcNow; // Set the modified date to the current date
            existingApplication.isDeleted = application.isDeleted;

            try
            {
                _context.SaveChanges();
                return Ok("Application updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating application: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteApplication(int id)
        {
            var application = _context.Application.Find(id);
            if (application == null)
            {
                return NotFound($"Application with ID {id} not found.");
            }

            try
            {
                _context.Application.Remove(application);
                _context.SaveChanges();
                return Ok($"Application with ID {id} has been deleted.");
            }
            catch (Exception ex)
            {
                // Log the exception here if you have a logging framework in place
                return BadRequest($"Error deleting application: {ex.Message}");
            }
        }



    }
}
