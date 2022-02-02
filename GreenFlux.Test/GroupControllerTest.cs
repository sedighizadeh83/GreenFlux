using AutoMapper;
using GreenFlux.API.Controllers;
using GreenFlux.DTO.Group;
using GreenFlux.ServiceAbstraction;
using GreenFlux.Service.Profiles;
using da = System.ComponentModel.DataAnnotations;
using GreenFlux.GlobalErrorHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using GreenFlux.Data.Models;

namespace GreenFlux.Test
{
    public class GroupControllerTest
    {
        private readonly GroupController _controller;
        private readonly IGroupService _service;
        public IMapper mapper;

        public GroupControllerTest()
        {
            var profiles = new List<Profile>();
            profiles.Add(new GroupProfile());
            profiles.Add(new ConnectorProfile());
            profiles.Add(new ChargeStationProfile());
            var config = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            mapper = config.CreateMapper();


            _service = new FakeGroupService(mapper);
            _controller = new GroupController(_service);
        }

        [Fact]
        public async void GetAllGroups_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = await _controller.GetAllGroups();

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async void GetAllGroups_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = await _controller.GetAllGroups() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<GroupReadWithDetailDto>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async void GetGroupById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = await _controller.GetGroupById(1000);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async void GetGroupById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = await _controller.GetGroupById(testId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async void GetGroupById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = await _controller.GetGroupById(testId) as OkObjectResult;

            // Assert
            Assert.IsType<GroupReadWithDetailDto>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as GroupReadWithDetailDto).Id);
        }

        [Fact]
        public void CreateGroup_InvalidObjectPassed_ReturnsValidationError()
        {
            // Arrange
            var invalidItem = new GroupCreateDto()
            {
                Name = "New Test Group",
                Capacity = 0
            };

            // Act
            var results = Validate(invalidItem);

            // Assert
            Assert.Equal(1, results.Count);
        }

        [Fact]
        public async void CreateGroup_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var validItem = new GroupCreateDto()
            {
                Name = "New Test Group",
                Capacity = 150
            };

            // Act
            var createdResponse = await _controller.CreateGroup(validItem);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse as OkObjectResult);
        }

        [Fact]
        public async void DeleteGroup_NotExistingIdPassed_ReturnsNotFoundException()
        {
            // Arrange
            var notExistingId = 1000;

            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await _controller.DeleteGroup(notExistingId));
        }

        [Fact]
        public async void DeleteGroup_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = 1;

            // Act
            var okResponse = await _controller.DeleteGroup(existingId);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse as OkObjectResult);
        }

        [Fact]
        public async void UpdateGroup_NotExistingIdPassed_ReturnsNotFoundException()
        {
            // Arrange
            var notExistingItem = new GroupUpdateDto()
            {
                Id = 1000,
                Name = "New Test Group",
                Capacity = 150
            };

            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await _controller.UpdateGroup(notExistingItem));
        }

        [Fact]
        public async void UpdateGroup_WhenViolatesCapacity_ReturnsValidationException()
        {
            var notValidItem = new GroupUpdateDto()
            {
                Id = 1,
                Name = "New Test Group",
                Capacity = 150
            };

            // Assert
            Assert.ThrowsAsync<ValidationException>(async () => _controller.UpdateGroup(notValidItem));
        }

        [Fact]
        public void UpdateGroup_InvalidObjectPassed_ReturnsValidationError()
        {
            // Arrange
            var invalidItem = new GroupUpdateDto()
            {
                Id = 1,
                Name = "New Test Group",
                Capacity = 0
            };

            // Act
            var results = Validate(invalidItem);

            // Assert
            Assert.Equal(1, results.Count);
        }

        [Fact]
        public async void UpdateGroup_ValidItemPassed_UpdatesItem()
        {
            // Arrange
            var validItem = new GroupUpdateDto()
            {
                Id = 1,
                Name = "New Test Group",
                Capacity = 450
            };

            // Act
            var okResponse = await _controller.UpdateGroup(validItem);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse as OkObjectResult);
        }

        public IList<da.ValidationResult> Validate(object dto)
        {
            var model = mapper.Map<Group>(dto);
            var results = new List<da.ValidationResult>();
            var validationContext = new da.ValidationContext(model, null, null);
            da.Validator.TryValidateObject(model, validationContext, results, true);
            if (model is da.IValidatableObject) (model as da.IValidatableObject).Validate(validationContext);
            return results;
        }
    }
}
