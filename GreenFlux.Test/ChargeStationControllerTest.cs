using AutoMapper;
using GreenFlux.API.Controllers;
using GreenFlux.DTO.ChargeStation;
using GreenFlux.ServiceAbstraction;
using GreenFlux.Service.Profiles;
using GreenFlux.GlobalErrorHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;
using GreenFlux.DTO.Connector;

namespace GreenFlux.Test
{
    public class ChargeStationControllerTest
    {
        private readonly ChargeStationController _controller;
        private readonly IChargeStationService _service;
        public IMapper mapper;

        public ChargeStationControllerTest()
        {
            var profiles = new List<Profile>();
            profiles.Add(new GroupProfile());
            profiles.Add(new ConnectorProfile());
            profiles.Add(new ChargeStationProfile());
            var config = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            mapper = config.CreateMapper();


            _service = new FakeChargeStationService(mapper);
            _controller = new ChargeStationController(_service);
        }

        [Fact]
        public void GetAllChargeStations_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetAllChargeStations();

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetAllChargeStations_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.GetAllChargeStations() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<ChargeStationReadWithDetailDto>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetChargeStationById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.GetChargeStationById(1000);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetChargeStationById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = _controller.GetChargeStationById(testId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetChargeStationById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = _controller.GetChargeStationById(testId) as OkObjectResult;

            // Assert
            Assert.IsType<ChargeStationReadWithDetailDto>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as ChargeStationReadWithDetailDto).Id);
        }

        [Fact]
        public void CreateChargeStation_NotExistingGroupIdPassed_ReturnsNotFoundException()
        {
            // Arrange
            var invalidItem = new ChargeStationCreateDto()
            {
                Name = "New Test Charge Station",
                GroupId = 1000,
                connectors = new List<ConnectorCreateIndirectDto>()
                {
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 1,
                        MaxCurrent = 100
                    }
                }
            };

            // Assert
            Assert.Throws<EntityNotFoundException>(() => _controller.CreateChargeStation(invalidItem));
        }

        [Fact]
        public void CreateChargeStation_WhenMoreThanFiveConnectorsPassed_ReturnsValidationException()
        {
            // Arrange
            var invalidItem = new ChargeStationCreateDto()
            {
                Name = "New Test Charge Station",
                GroupId = 2,
                connectors = new List<ConnectorCreateIndirectDto>()
                {
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 1,
                        MaxCurrent = 100
                    },
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 2,
                        MaxCurrent = 100
                    },
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 3,
                        MaxCurrent = 100
                    },
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 4,
                        MaxCurrent = 100
                    },
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 5,
                        MaxCurrent = 100
                    },
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 6,
                        MaxCurrent = 100
                    }
                }
            };

            // Assert
            Assert.Throws<ValidationException>(() => _controller.CreateChargeStation(invalidItem));
        }

        [Fact]
        public void CreateChargeStation_WhenViolatesCapacity_ReturnsValidationException()
        {
            // Arrange
            var invalidItem = new ChargeStationCreateDto()
            {
                Name = "New Test Charge Station",
                GroupId = 2,
                connectors = new List<ConnectorCreateIndirectDto>()
                {
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 1,
                        MaxCurrent = 100
                    },
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 2,
                        MaxCurrent = 100
                    },
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 3,
                        MaxCurrent = 100
                    },
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 4,
                        MaxCurrent = 100
                    }
                }
            };

            // Assert
            Assert.Throws<ValidationException>(() => _controller.CreateChargeStation(invalidItem));
        }

        [Fact]
        public void CreateChargeStation_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var validItem = new ChargeStationCreateDto()
            {
                Name = "New Test Charge Station",
                GroupId = 3,
                connectors = new List<ConnectorCreateIndirectDto>()
                {
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 1,
                        MaxCurrent = 1
                    },
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 2,
                        MaxCurrent = 2
                    },
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 3,
                        MaxCurrent = 3
                    },
                    new ConnectorCreateIndirectDto()
                    {
                        Id = 4,
                        MaxCurrent = 4
                    }
                }
            };

            // Act
            var createdResponse = _controller.CreateChargeStation(validItem);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse as OkObjectResult);
        }

        [Fact]
        public void DeleteChargeStation_NotExistingIdPassed_ReturnsNotFoundException()
        {
            // Arrange
            var notExistingId = 1000;

            // Assert
            Assert.Throws<EntityNotFoundException>(() => _controller.DeleteChargeStation(notExistingId));
        }

        [Fact]
        public void DeleteChargeStation_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = 1;

            // Act
            var okResponse = _controller.DeleteChargeStation(existingId);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse as OkObjectResult);
        }

        [Fact]
        public void UpdateChargeStation_NotExistingIdPassed_ReturnsNotFoundException()
        {
            // Arrange
            var notExistingItem = new ChargeStationUpdateDto()
            {
                Id = 1000,
                Name = "New Test Charge Station",
                GroupId = 1
            };

            // Assert
            Assert.Throws<EntityNotFoundException>(() => _controller.UpdateChargeStation(notExistingItem));
        }

        [Fact]
        public void UpdateChargeStation_ValidItemPassed_UpdatesItem()
        {
            // Arrange
            var validItem = new ChargeStationUpdateDto()
            {
                Id = 1,
                Name = "New Test Charge Station",
                GroupId = 1
            };

            // Act
            var okResponse = _controller.UpdateChargeStation(validItem);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse as OkObjectResult);
        }
    }
}
