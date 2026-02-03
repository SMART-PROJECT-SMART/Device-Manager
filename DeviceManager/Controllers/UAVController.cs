
using Microsoft.AspNetCore.Mvc;
using DeviceManager.Common.Constants;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using DeviceManager.Services.UAVDBService.Interfaces;

namespace DeviceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UAVController : ControllerBase
    {
        private readonly IUAVService _uavService;

        public UAVController(IUAVService uavService)
        {
            _uavService = uavService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UAVRo>>> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<UAVRo> uavs = await _uavService.GetAllUAVsAsync(cancellationToken);
            return Ok(uavs);
        }

        [HttpGet("{tailId:int}")]
        public async Task<ActionResult<UAVRo>> GetByTailId(int tailId, CancellationToken cancellationToken)
        {
            UAVRo uav = await _uavService.GetUAVByTailIdAsync(tailId, cancellationToken);
            if (uav == null)
            {
                return NotFound(string.Format(DeviceManagerConstants.ErrorMessages.UAV_NOT_FOUND, tailId));
            }
            return Ok(uav);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateUAVDTO createUAVDTO, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool created = await _uavService.CreateUAVAsync(createUAVDTO, cancellationToken);
            if (!created)
            {
                return BadRequest(DeviceManagerConstants.ErrorMessages.UAV_CREATE_FAILED);
            }
            return CreatedAtAction(nameof(GetByTailId), new { tailId = createUAVDTO.TailId }, null);
        }

        [HttpPut("{tailId:int}")]
        public async Task<ActionResult> Update(int tailId, [FromBody] UpdateUAVDto updateUAVDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool updated = await _uavService.UpdateUAVAsync(tailId, updateUAVDto, cancellationToken);
            if (!updated)
            {
                return NotFound(string.Format(DeviceManagerConstants.ErrorMessages.UAV_NOT_FOUND, tailId));
            }
            return NoContent();
        }

        [HttpDelete("{tailId:int}")]
        public async Task<ActionResult> Delete(int tailId, CancellationToken cancellationToken)
        {
            bool deleted = await _uavService.DeleteUAVByTailIdAsync(tailId, cancellationToken);
            if (!deleted)
            {
                return NotFound(string.Format(DeviceManagerConstants.ErrorMessages.UAV_NOT_FOUND, tailId));
            }
            return NoContent();
        }
    }
}
