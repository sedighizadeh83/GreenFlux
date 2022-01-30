using Microsoft.AspNetCore.Mvc;
using GreenFlux.DTO.ChargeStation;
using GreenFlux.ServiceAbstraction;
using System.Collections.Generic;

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
        public ActionResult GetAllChargeStations()
        {
            var chargeStationItems = _chargeStationService.GetAllChargeStations();

            return Ok(chargeStationItems);
        }

        [HttpGet("{id}")]
        public ActionResult GetChargeStationById(int id)
        {
            var chargeStationItem = _chargeStationService.GetChargeStationById(id);

            if (chargeStationItem == null)
            {
                return NotFound();
            }

            return Ok(chargeStationItem);
        }

        [HttpPost]
        public ActionResult CreateChargeStation(ChargeStationCreateDto chargeStation)
        {
            _chargeStationService.CreateChargeStation(chargeStation);

            return Ok("Charge station created successfully");
        }

        [HttpPut]
        public ActionResult UpdateChargeStation(ChargeStationUpdateDto chargeStation)
        {
            _chargeStationService.UpdateChargeStation(chargeStation);

            return Ok("Charge station updated successfully");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteChargeStation(int id)
        {
            _chargeStationService.DeleteChargeStation(id);

            return Ok("Charge station deleted successfully");
        }
    }
}
