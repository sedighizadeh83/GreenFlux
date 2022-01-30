using AutoMapper;
using GreenFlux.API.Controllers;
using GreenFlux.DTO.ChargeStation;
using GreenFlux.ServiceAbstraction;
using GreenFlux.Service.Profiles;
using da = System.ComponentModel.DataAnnotations;
using GreenFlux.GlobalErrorHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;
using GreenFlux.Data.Models;
using GreenFlux.DTO.Connector;

namespace GreenFlux.Test
{
    public class ConnectorControllerTest
    {
        private readonly ConnectorController _controller;
        private readonly IConnectorService _service;
        public IMapper mapper;

        public ConnectorControllerTest()
        {
            var profiles = new List<Profile>();
            profiles.Add(new GroupProfile());
            profiles.Add(new ConnectorProfile());
            profiles.Add(new ChargeStationProfile());
            var config = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            mapper = config.CreateMapper();

            _service = new FakeConnectorService(mapper);
            _controller = new ConnectorController(_service);
        }

        [Fact]
        public void GetAllConnectors_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetAllConnectors();

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetAllConnectors_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.GetAllConnectors() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<ConnectorReadWithDetailDto>>(okResult.Value);
            Assert.Equal(8, items.Count);
        }

        [Fact]
        public void GetConnectorById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.GetConnectorById(1000, 1);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetConnectorById_UnknownChargeStationIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.GetConnectorById(1, 1000);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetConnectorById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testId = 1;
            var chargeStationId = 1;

            // Act
            var okResult = _controller.GetConnectorById(testId, chargeStationId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetConnectorById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 1;
            var chargeStationId = 1;

            // Act
            var okResult = _controller.GetConnectorById(testId, chargeStationId) as OkObjectResult;

            // Assert
            Assert.IsType<ConnectorReadWithDetailDto>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as ConnectorReadWithDetailDto).Id);
            Assert.Equal(chargeStationId, (okResult.Value as ConnectorReadWithDetailDto).ChargeStation.Id);
        }

        [Fact]
        public void CreateConnector_NotExistingChargeStationIdPassed_ReturnsNotFoundException()
        {
            // Arrange
            var invalidItem = new ConnectorCreateDto()
            {
                Id = 3,
                ChargeStationId = 1000,
                MaxCurrent = 100
            };

            // Assert
            Assert.Throws<EntityNotFoundException>(() => _controller.CreateConnector(invalidItem));
        }

        [Fact]
        public void CreateConnector_WhenViolatesConnectorsMaxNumber_ReturnsValidationException()
        {
            // Arrange
            var invalidItem = new ConnectorCreateDto()
            {
                Id = 5,
                ChargeStationId = 3,
                MaxCurrent = 100
            };

            // Assert
            Assert.Throws<ValidationException>(() => _controller.CreateConnector(invalidItem));
        }

        [Fact]
        public void CreateConnector_WhenViolatesCapacity_ReturnsValidationException()
        {
            // Arrange
            var invalidItem = new ConnectorCreateDto()
            {
                Id = 2,
                ChargeStationId = 2,
                MaxCurrent = 100
            };

            // Assert
            Assert.Throws<ValidationException>(() => _controller.CreateConnector(invalidItem));
        }

        [Fact]
        public void CreateConnector_InvalidObjectPassed_ReturnsValidationError()
        {
            // Arrange
            var invalidItem = new ConnectorCreateDto()
            {
                Id = 2,
                ChargeStationId = 2,
                MaxCurrent = 0
            };

            // Act
            var results = Validate(invalidItem);

            // Assert
            Assert.Equal(1, results.Count);
        }

        [Fact]
        public void CreateConnector_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var validItem = new ConnectorCreateDto()
            {
                Id = 2,
                ChargeStationId = 2,
                MaxCurrent = 5
            };

            // Act
            var createdResponse = _controller.CreateConnector(validItem);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse as OkObjectResult);
        }

        [Fact]
        public void DeleteConnector_NotExistingIdPassed_ReturnsNotFoundException()
        {
            // Arrange
            var notExistingId = 1000;

            // Assert
            Assert.Throws<EntityNotFoundException>(() => _controller.DeleteConnector(notExistingId, 1));
        }

        [Fact]
        public void DeleteConnector_NotExistingCharheStationIdPassed_ReturnsNotFoundException()
        {
            // Arrange
            var notExistingChargeStationId = 1000;

            // Assert
            Assert.Throws<EntityNotFoundException>(() => _controller.DeleteConnector(1, notExistingChargeStationId));
        }

        [Fact]
        public void DeleteConnector_WhenViolatesConnectorsMinNumber_ReturnsValidationException()
        {
            // Arrange
            var existingId = 1;
            var existingChargeStationId = 2;

            // Assert
            Assert.Throws<ValidationException>(() => _controller.DeleteConnector(existingId, existingChargeStationId));
        }

        [Fact]
        public void DeleteConnector_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = 1;
            var existingChargeStationId = 1;

            // Act
            var okResponse = _controller.DeleteConnector(existingId, existingChargeStationId);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse as OkObjectResult);
        }

        [Fact]
        public void UpdateConnector_NotExistingIdPassed_ReturnsNotFoundException()
        {
            // Arrange
            var notExistingItem = new ConnectorUpdateDto()
            {
                Id = 1000,
                ChargeStationId = 1,
                MaxCurrent = 100
            };

            // Assert
            Assert.Throws<EntityNotFoundException>(() => _controller.UpdateConnector(notExistingItem));
        }

        [Fact]
        public void UpdateConnector_NotExistingChargeStationIdPassed_ReturnsNotFoundException()
        {
            // Arrange
            var notExistingItem = new ConnectorUpdateDto()
            {
                Id = 1,
                ChargeStationId = 1000,
                MaxCurrent = 100
            };

            // Assert
            Assert.Throws<EntityNotFoundException>(() => _controller.UpdateConnector(notExistingItem));
        }

        [Fact]
        public void UpdateConnector_InvalidObjectPassed_ReturnsValidationError()
        {
            // Arrange
            var invalidItem = new ConnectorUpdateDto()
            {
                Id = 1,
                ChargeStationId = 2,
                MaxCurrent = 0
            };

            // Act
            var results = Validate(invalidItem);

            // Assert
            Assert.Equal(1, results.Count);
        }

        [Fact]
        public void UpdateConnector_ValidItemPassed_UpdatesItem()
        {
            // Arrange
            var validItem = new ConnectorUpdateDto()
            {
                Id = 1,
                ChargeStationId = 2,
                MaxCurrent = 90
            };

            // Act
            var okResponse = _controller.UpdateConnector(validItem);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse as OkObjectResult);
        }

        public IList<da.ValidationResult> Validate(object dto)
        {
            var model = mapper.Map<Connector>(dto);
            var results = new List<da.ValidationResult>();
            var validationContext = new da.ValidationContext(model, null, null);
            da.Validator.TryValidateObject(model, validationContext, results, true);
            if (model is da.IValidatableObject) (model as da.IValidatableObject).Validate(validationContext);
            return results;
        }
    }
}
