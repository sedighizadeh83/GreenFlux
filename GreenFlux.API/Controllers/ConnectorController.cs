using Microsoft.AspNetCore.Mvc;
using GreenFlux.DTO.Connector;
using GreenFlux.ServiceAbstraction;
using System.Collections.Generic;

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
        public ActionResult GetAllConnectors()
        {
            var connectorItems = _connectorService.GetAllConnectors();

            return Ok(connectorItems);
        }

        [HttpGet("{id}/{chargeStationId}")]
        public ActionResult GetConnectorById(int id, int chargeStationId)
        {
            var connectorItem = _connectorService.GetConnectorById(id, chargeStationId);

            if (connectorItem == null)
            {
                return NotFound();
            }

            return Ok(connectorItem);
        }

        [HttpPost]
        public ActionResult CreateConnector(ConnectorCreateDto connector)
        {
            _connectorService.CreateConnector(connector);

            return Ok("Connector created successfully");
        }

        [HttpPut]
        public ActionResult UpdateConnector(ConnectorUpdateDto connector)
        {
            _connectorService.UpdateConnector(connector);

            return Ok("Connector updated successfully");
        }

        [HttpDelete("{id}/{chargeStationId}")]
        public ActionResult DeleteConnector(int id, int chargeStationId)
        {
            _connectorService.DeleteConnector(id, chargeStationId);

            return Ok("Connector deleted successfully");
        }
    }
}
