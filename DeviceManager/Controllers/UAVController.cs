
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeviceManager.Database.MongoDB.Services.UAVDBService.Interfaces;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;

namespace DeviceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UAVController : ControllerBase
    {
        private readonly IUAVDBService uavDbService;

        public UAVController(IUAVDBService uavDbService)
        {
            this.uavDbService = uavDbService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UAVRo>>> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<UAVRo> uavs = await uavDbService.GetAllUAVsAsync(cancellationToken);
            return Ok(uavs);
        }

        [HttpGet("{tailId}")]
        public async Task<ActionResult<UAVRo>> GetByTailId(int tailId, CancellationToken cancellationToken)
        {
            UAVRo uav = await uavDbService.GetUAVByTailIdAsync(tailId, cancellationToken);
            if (uav == null)
            {
                return NotFound();
            }
            return Ok(uav);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateUAVDTO createUAVDTO, CancellationToken cancellationToken)
        {
            bool created = await uavDbService.CreateUAVAsync(createUAVDTO, cancellationToken);
            if (!created)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetByTailId), new { tailId = createUAVDTO.TailId }, null);
        }

        [HttpPut("{tailId}")]
        public async Task<ActionResult> Update(int tailId, [FromBody] UpdateUAVDto updateUAVDto, CancellationToken cancellationToken)
        {
            bool updated = await uavDbService.UpdateUAVAsync(tailId, updateUAVDto, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{tailId}")]
        public async Task<ActionResult> Delete(int tailId, CancellationToken cancellationToken)
        {
            bool deleted = await uavDbService.DeleteUAVByTailIdAsync(tailId, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
