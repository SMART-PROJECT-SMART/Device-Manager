using Microsoft.AspNetCore.Mvc;
using Core.Common.Enums;
using DeviceManager.Database.MongoDB.Services.SleeveDBService;
using DeviceManager.Models.Dto;
using DeviceManager.Models.Ro;
using DeviceManager.Services.TelemetryDeviceNotification;

namespace DeviceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SleeveController : ControllerBase
    {
        private readonly ISleeveDBService _sleeveDbService;
        private readonly ITelemetryDeviceNotificationService _telemetryDeviceNotificationService;

        public SleeveController(ISleeveDBService sleeveDbService, ITelemetryDeviceNotificationService telemetryDeviceNotificationService)
        {
            _sleeveDbService = sleeveDbService;
            _telemetryDeviceNotificationService = telemetryDeviceNotificationService;
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
            _ = _telemetryDeviceNotificationService.NotifySleeveChangedAsync(CrudOperation.Created, createSleeveDTO.Name, cancellationToken);
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
            _ = _telemetryDeviceNotificationService.NotifySleeveChangedAsync(CrudOperation.Updated, name, cancellationToken);
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
            _ = _telemetryDeviceNotificationService.NotifySleeveChangedAsync(CrudOperation.Deleted, name, cancellationToken);
            return NoContent();
        }
    }
}
