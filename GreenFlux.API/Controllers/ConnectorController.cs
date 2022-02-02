using Microsoft.AspNetCore.Mvc;
using GreenFlux.DTO.Connector;
using GreenFlux.ServiceAbstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenFlux.API.Controllers
{
    [Route("api/connectors")]
    [ApiController]
    public class ConnectorController : ControllerBase
    {
        private readonly IConnectorService _connectorService;

        public ConnectorController(IConnectorService connectorService)
        {
            _connectorService = connectorService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllConnectors()
        {
            var connectorItems = await _connectorService.GetAllConnectors();

            return Ok(connectorItems);
        }

        [HttpGet("{id}/{chargeStationId}")]
        public async Task<ActionResult> GetConnectorById(int id, int chargeStationId)
        {
            var connectorItem = await _connectorService.GetConnectorById(id, chargeStationId);

            if (connectorItem == null)
            {
                return NotFound();
            }

            return Ok(connectorItem);
        }

        [HttpPost]
        public async Task<ActionResult> CreateConnector(ConnectorCreateDto connector)
        {
            await _connectorService.CreateConnector(connector);

            return Ok("Connector created successfully");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateConnector(ConnectorUpdateDto connector)
        {
            await _connectorService.UpdateConnector(connector);

            return Ok("Connector updated successfully");
        }

        [HttpDelete("{id}/{chargeStationId}")]
        public async Task<ActionResult> DeleteConnector(int id, int chargeStationId)
        {
            await _connectorService.DeleteConnector(id, chargeStationId);

            return Ok("Connector deleted successfully");
        }
    }
}
