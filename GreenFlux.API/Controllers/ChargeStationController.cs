using Microsoft.AspNetCore.Mvc;
using GreenFlux.DTO.ChargeStation;
using GreenFlux.ServiceAbstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenFlux.API.Controllers
{
    [Route("api/chargestations")]
    [ApiController]
    public class ChargeStationController : ControllerBase
    {
        private readonly IChargeStationService _chargeStationService;

        public ChargeStationController(IChargeStationService chargeStationService)
        {
            _chargeStationService = chargeStationService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllChargeStations()
        {
            var chargeStationItems = await _chargeStationService.GetAllChargeStations();

            return Ok(chargeStationItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetChargeStationById(int id)
        {
            var chargeStationItem = await _chargeStationService.GetChargeStationById(id);

            if (chargeStationItem == null)
            {
                return NotFound();
            }

            return Ok(chargeStationItem);
        }

        [HttpPost]
        public async Task<ActionResult> CreateChargeStation(ChargeStationCreateDto chargeStation)
        {
            await _chargeStationService.CreateChargeStation(chargeStation);

            return Ok("Charge station created successfully");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateChargeStation(ChargeStationUpdateDto chargeStation)
        {
            await _chargeStationService.UpdateChargeStation(chargeStation);

            return Ok("Charge station updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChargeStation(int id)
        {
            await _chargeStationService.DeleteChargeStation(id);

            return Ok("Charge station deleted successfully");
        }
    }
}
