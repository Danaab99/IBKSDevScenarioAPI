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

        [HttpPut]
        public IActionResult DeleteApplication(Application application) {

            if (application == null || application.Id == 0)
            {

                if (application == null)
                {
                    return BadRequest("Application data is invalid.");

                }

                else if (application.Id == 0) {

                    return BadRequest("Application id is invalid.");
                }
            }

            try
            {
                var approved_Application = _context.Application.Find(application.Id);
                if (application == null)
                {
                    return NotFound("Application not found.");
                }
                approved_Application.AppStatus = application.AppStatus;
                approved_Application.ProjectRef = application.ProjectRef;
                approved_Application.ProjectName = application.ProjectName;
                approved_Application.ProjectLocation = application.ProjectLocation;
                approved_Application.OpenDt = application.OpenDt;
                approved_Application.StartDt = application.StartDt;
                approved_Application.CompletedDt = application.CompletedDt;
                approved_Application.ProjectValue = application.ProjectValue;
                approved_Application.StatusId = application.StatusId;
                approved_Application.Notes = application.Notes;
                approved_Application.Modified = application.Modified;
                approved_Application.isDeleted = application.isDeleted;
                _context.SaveChanges();
                return Ok("Application details updated.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
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
