
using Microsoft.AspNetCore.Mvc;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using DeviceManager.Services.UAVDBService.Interfaces;

namespace DeviceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UAVController : ControllerBase
    {
        private readonly IUAVService _uavDbService;

        public UAVController(IUAVService uavDbService)
        {
            _uavDbService = uavDbService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UAVRo>>> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<UAVRo> uavs = await _uavDbService.GetAllUAVsAsync(cancellationToken);
            return Ok(uavs);
        }

        [HttpGet("{tailId}")]
        public async Task<ActionResult<UAVRo>> GetByTailId(int tailId, CancellationToken cancellationToken)
        {
            UAVRo uav = await _uavDbService.GetUAVByTailIdAsync(tailId, cancellationToken);
            if (uav == null)
            {
                return NotFound();
            }
            return Ok(uav);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateUAVDTO createUAVDTO, CancellationToken cancellationToken)
        {
            bool created = await _uavDbService.CreateUAVAsync(createUAVDTO, cancellationToken);
            if (!created)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetByTailId), new { tailId = createUAVDTO.TailId }, null);
        }

        [HttpPut("{tailId}")]
        public async Task<ActionResult> Update(int tailId, [FromBody] UpdateUAVDto updateUAVDto, CancellationToken cancellationToken)
        {
            bool updated = await _uavDbService.UpdateUAVAsync(tailId, updateUAVDto, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{tailId}")]
        public async Task<ActionResult> Delete(int tailId, CancellationToken cancellationToken)
        {
            bool deleted = await _uavDbService.DeleteUAVByTailIdAsync(tailId, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
