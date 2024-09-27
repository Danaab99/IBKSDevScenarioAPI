using IBKSDevScenarioAPI.DAL;
using IBKSDevScenarioAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task <IActionResult> GetAllInquiries()
        {
            try
            {
                var inquiries = await _context.Application.ToListAsync();
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
        public async Task<IActionResult> GetInquiry(int id)
        {
            try
            {
                var inquiry = await _context.Inquries.FindAsync(id);
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
        public async Task<IActionResult> PostInquiry([FromBody] Inquries inquiry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Sends back a 400 error with details about what went wrong
            }

            _context.Inquries.Add(inquiry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInquiry), new { id = inquiry.Id }, inquiry);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInquiry(int id, [FromBody] Inquries updatedInquiry)
        {
            if (id != updatedInquiry.Id)
            {
                return BadRequest("ID mismatch in the URL and the inquiry object.");
            }

            var inquiry =await _context.Inquries.FindAsync(id);
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
                await _context.SaveChangesAsync();

                return Ok("Inquiry updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating inquiry: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInquiry(int id)
        {
            var inquiry = await _context.Inquries.FindAsync(id);
            if (inquiry == null)
            {
                return NotFound($"Inquiry with ID {id} not found.");
            }

            try
            {
                _context.Inquries.Remove(inquiry);
                await _context.SaveChangesAsync();
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
