using ApartmentPriceTracker.Api.Controllers;
using ApartmentPriceTracker.Api.Models;
using ApartmentPriceTracker.Api.Services.Interfaces;
using ApartmentPriceTracker.Core.DTOs;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentPriceTracker.Tests.Controllers
{
    public class ApartmentControllerTests
    {
        private readonly IApartmentService _service;
        public ApartmentControllerTests() { _service = A.Fake<IApartmentService>(); }
        [Fact]
        public void GetApartmentPrices_WithApartments_ShouldReturnOk()
        {
            // Arrange
            A.CallTo(() => _service.GetApartments()).Returns(new List<Apartment>
            {
                new Apartment { ApartmentUrl = "url1", Price = "8 559 000 ₽" },
                new Apartment { ApartmentUrl = "url2", Price = "7 500 000 ₽" }
            });

            var controller = new ApartmentController(_service);

            // Act
            var result = controller.GetApartmentPrices();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<IEnumerable<Apartment>>();
        }

        [Fact]
        public void GetApartmentPrices_WithNoApartments_ShouldReturnNotFound()
        {
            // Arrange
            A.CallTo(() => _service.GetApartments()).Returns(new List<Apartment>());

            var controller = new ApartmentController(_service);

            // Act
            var result = controller.GetApartmentPrices();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void Subscribe_WithValidSubscription_ShouldReturnOk()
        {
            // Arrange
            var controller = new ApartmentController(_service);
            var validSubscription = new Subscription { ApartmentUrl = "newUrl", Email = "newEmail" };

            // Act
            var result = await controller.Subscribe(validSubscription) as OkResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void Subscribe_WithInvalidSubscription_ShouldReturnBadRequest()
        {
            // Arrange
            var controller = new ApartmentController(_service);
            var invalidSubscription = new Subscription();
            controller.ModelState.AddModelError("PropertyName", "Error Message");

            // Act
            var result = await controller.Subscribe(invalidSubscription) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }
    }
}
