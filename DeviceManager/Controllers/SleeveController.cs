using Microsoft.AspNetCore.Mvc;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using DeviceManager.Services.SleeveDBService.Interfaces;

namespace DeviceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SleeveController : ControllerBase
    {
        private readonly ISleeveService _sleeveDbService;

        public SleeveController(ISleeveService sleeveDbService)
        {
            _sleeveDbService = sleeveDbService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SleeveRo>>> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<SleeveRo> sleeves = await _sleeveDbService.GetAllSleevesAsync(cancellationToken);
            return Ok(sleeves);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<SleeveRo>> GetByName(string name, CancellationToken cancellationToken)
        {
            SleeveRo sleeve = await _sleeveDbService.GetSleeveByNameAsync(name, cancellationToken);
            if (sleeve == null)
            {
                return NotFound();
            }
            return Ok(sleeve);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateSleeveDTO createSleeveDTO, CancellationToken cancellationToken)
        {
            bool created = await _sleeveDbService.CreateSleeveAsync(createSleeveDTO, cancellationToken);
            if (!created)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetByName), new { name = createSleeveDTO.Name }, null);
        }

        [HttpPut("{name}")]
        public async Task<ActionResult> Update(string name, [FromBody] UpdateSleeveDTO updateSleeveDTO, CancellationToken cancellationToken)
        {
            bool updated = await _sleeveDbService.UpdateSleeveAsync(name, updateSleeveDTO, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(string name, CancellationToken cancellationToken)
        {
            bool deleted = await _sleeveDbService.DeleteSleeveByNameAsync(name, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("available-for-uav/{tailId}")]
        public async Task<ActionResult<IEnumerable<int>>> GetAvailableForUAV(int tailId, CancellationToken cancellationToken)
        {
            IEnumerable<int> ports = await _sleeveDbService.GetAvailableSleeveForUAVAsync(tailId, cancellationToken);
            if (ports == null || !ports.Any())
            {
                return NotFound();
            }
            return Ok(ports);
        }

        [HttpPost("release/{tailId}")]
        public async Task<ActionResult> ReleaseByTailId(int tailId, CancellationToken cancellationToken)
        {
            bool released = await _sleeveDbService.ReleaseSleeveByTailIdAsync(tailId, cancellationToken);
            if (!released)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
