using IBKSDevScenarioAPI.DAL;
using IBKSDevScenarioAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IBKSDevScenarioAPI.Controllers
{
    [Route("api/Inquries")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        private readonly DevScenarioDbContext _context;

        public InquiryController(DevScenarioDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllInquiries()
        {
            try
            {
                var inquiries = _context.Inquries.ToList();
                if (inquiries.Count == 0)
                {
                    return NotFound("No Inquiries found.");
                }
                return Ok(inquiries);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetInquiry(int id)
        {
            try
            {
                var inquiry = _context.Inquries.Find(id);
                if (inquiry == null)
                {
                    return NotFound("Application no found.");

                }
                return Ok(inquiry);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostInquiry(Inquries inquiry)
        {
            try
            {
                _context.Inquries.Add(inquiry); // Ensure 'level' does not have an Id set explicitly
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetInquiry), new { id = inquiry.Id }, inquiry);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInquiry(int id, [FromBody] Inquries updatedInquiry)
        {
            if (id != updatedInquiry.Id)
            {
                return BadRequest("ID mismatch in the URL and the inquiry object.");
            }

            var inquiry = _context.Inquries.Find(id);
            if (inquiry == null)
            {
                return NotFound($"Inquiry with ID {id} not found.");
            }

            try
            {
                // Update the properties
                inquiry.SendToPerson = updatedInquiry.SendToPerson;
                inquiry.SendToRole = updatedInquiry.SendToRole;
                inquiry.SendToPersonId = updatedInquiry.SendToPersonId;
                inquiry.Subject = updatedInquiry.Subject;
                inquiry.Inquiry = updatedInquiry.Inquiry;
                inquiry.Response = updatedInquiry.Response;
                inquiry.AskedDt = updatedInquiry.AskedDt;
                inquiry.CompletedDt = updatedInquiry.CompletedDt;

                _context.Inquries.Update(inquiry);
                _context.SaveChanges();

                return Ok("Inquiry updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating inquiry: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInquiry(int id)
        {
            var inquiry = _context.Inquries.Find(id);
            if (inquiry == null)
            {
                return NotFound($"Inquiry with ID {id} not found.");
            }

            try
            {
                _context.Inquries.Remove(inquiry);
                _context.SaveChanges();
                return Ok($"Inquiry with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception here if you have a logging framework in place
                return BadRequest($"Error deleting inquiry: {ex.Message}");
            }
        }

    }
}
