using Microsoft.AspNetCore.Mvc;
using DeviceManager.Common.Constants;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using DeviceManager.Services.SleeveService.Interfaces;

namespace DeviceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SleeveController : ControllerBase
    {
        private readonly ISleeveService _sleeveService;

        public SleeveController(ISleeveService sleeveService)
        {
            _sleeveService = sleeveService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SleeveRo>>> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<SleeveRo> sleeves = await _sleeveService.GetAllSleevesAsync(cancellationToken);
            return Ok(sleeves);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<SleeveRo>> GetByName(string name, CancellationToken cancellationToken)
        {
            SleeveRo sleeve = await _sleeveService.GetSleeveByNameAsync(name, cancellationToken);
            if (sleeve == null)
            {
                return NotFound(string.Format(DeviceManagerConstants.ErrorMessages.SLEEVE_NOT_FOUND, name));
            }
            return Ok(sleeve);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateSleeveDTO createSleeveDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool created = await _sleeveService.CreateSleeveAsync(createSleeveDTO, cancellationToken);
            if (!created)
            {
                return BadRequest(DeviceManagerConstants.ErrorMessages.SLEEVE_CREATE_FAILED);
            }
            return CreatedAtAction(nameof(GetByName), new { name = createSleeveDTO.Name }, null);
        }

        [HttpPut("{name}")]
        public async Task<ActionResult> Update(string name, [FromBody] UpdateSleeveDTO updateSleeveDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool updated = await _sleeveService.UpdateSleeveAsync(name, updateSleeveDTO, cancellationToken);
            if (!updated)
            {
                return NotFound(string.Format(DeviceManagerConstants.ErrorMessages.SLEEVE_NOT_FOUND, name));
            }
            return NoContent();
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(string name, CancellationToken cancellationToken)
        {
            bool deleted = await _sleeveService.DeleteSleeveByNameAsync(name, cancellationToken);
            if (!deleted)
            {
                return NotFound(string.Format(DeviceManagerConstants.ErrorMessages.SLEEVE_NOT_FOUND, name));
            }
            return NoContent();
        }

        [HttpGet("available-for-uav/{tailId:int}")]
        public async Task<ActionResult<IEnumerable<int>>> GetAvailableForUAV(int tailId, CancellationToken cancellationToken)
        {
            IEnumerable<int> ports = await _sleeveService.GetAvailableSleeveForUAVAsync(tailId, cancellationToken);
            if (ports == null || !ports.Any())
            {
                return NotFound(string.Format(DeviceManagerConstants.ErrorMessages.NO_AVAILABLE_SLEEVE, tailId));
            }
            return Ok(ports);
        }

        [HttpPost("release/{tailId:int}")]
        public async Task<ActionResult> ReleaseByTailId(int tailId, CancellationToken cancellationToken)
        {
            bool released = await _sleeveService.ReleaseSleeveByTailIdAsync(tailId, cancellationToken);
            if (!released)
            {
                return NotFound(string.Format(DeviceManagerConstants.ErrorMessages.SLEEVE_RELEASE_FAILED, tailId));
            }
            return NoContent();
        }

        [HttpPost("assign")]
        public async Task<ActionResult> Assign([FromBody] AssignSleeveToUavDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool assigned = await _sleeveService.AssignSleeveToUavAsync(dto.TailId, dto.SleeveId, cancellationToken);
            if (!assigned)
            {
                return NotFound(string.Format(DeviceManagerConstants.ErrorMessages.SLEEVE_ID_NOT_FOUND, dto.SleeveId));
            }
            return NoContent();
        }
    }
}
