using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using req_tracker_back.Models;
using req_tracker_back.Services;
using req_tracker_back.ViewModels;

namespace req_tracker_back.Controllers
{
    [Route("RT/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController(TicketsService service) : ControllerBase
    {
        private readonly TicketsService _service = service;

        [HttpGet]
        public ActionResult<IEnumerable<TicketDTO>> GetAll([FromQuery]string? filter)
        {
            try
            {
                return Ok(_service.GetAll(filter));
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<TicketDTO> Get(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]TicketDTO request)
        {
            try
            {
                int id = _service.Create(request);
                return Ok(new { id = id });
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }

        }

        [HttpPut]
        public IActionResult Update([FromBody] TicketDTO ticket)
        {
            try
            {
                _service.Update(ticket);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }

        }

        [HttpGet("statuses")]
        public ActionResult<IEnumerable<Status>> GetAllStatuses()
        {
            try
            {
                return Ok(_service.GetAllStatuses());
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
