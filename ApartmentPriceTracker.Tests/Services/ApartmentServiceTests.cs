using ApartmentPriceTracker.Api.Models;
using ApartmentPriceTracker.Api.Services;
using ApartmentPriceTracker.Api.Services.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentPriceTracker.Tests.Services
{
    public class ApartmentServiceTests
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private readonly IHtmlParserService _htmlParserService;
        public ApartmentServiceTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _htmlParserService = A.Fake<IHtmlParserService>();
        }
        [Fact]
        public void GetApartments_ShouldReturnApartmentsWithPrices()
        {
            // Arrange
            A.CallTo(() => _htmlParserService.GetApartmentPrice("https://example1.com")).Returns("1 000 000 ₽");
            A.CallTo(() => _htmlParserService.GetApartmentPrice("https://example2.com")).Returns("2 000 000 ₽");

            var apartmentService = new ApartmentService(GetDbContext(), _htmlParserService);

            // Act
            var result = apartmentService.GetApartments();

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Should().Contain(apartment => apartment.ApartmentUrl == "https://example1.com" && apartment.Price == "1 000 000 ₽");
            result.Should().Contain(apartment => apartment.ApartmentUrl == "https://example2.com" && apartment.Price == "2 000 000 ₽");
        }

        [Fact]
        public async Task SaveSubscriptionAsync_NewSubscription_ShouldAddSubscriptionToContext()
        {
            // Arrange
            var context = GetDbContext();
            var apartmentService = new ApartmentService(context, _htmlParserService);

            // Act & Assert
            await apartmentService.Invoking(async a => await a.SaveSubscriptionAsync("newUrl", "newEmail"))
                .Should().NotThrowAsync();
            context.Subscriptions.Should().HaveCount(3);
            context.Subscriptions.Should().Contain(s => s.ApartmentUrl == "newUrl" && s.Email == "newEmail");
        }

        [Fact]
        public async Task SaveSubscriptionAsync_ExistingSubscription_ShouldThrowException()
        {
            // Arrange
            var apartmentService = new ApartmentService(GetDbContext(), _htmlParserService);

            // Act & Assert
            await apartmentService.Invoking(async p => await p.SaveSubscriptionAsync("https://example1.com", "user@example.com"))
                .Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Подписка по user@example.com на объявлению https://example1.com уже оформлена");
        }
        private AppDbContext GetDbContext()
        {
            var context = new AppDbContext(_options);
            context.Subscriptions.AddRange(GetSubscriptionsForTest());
            context.SaveChanges();
            return context;
        }
        private Subscription[] GetSubscriptionsForTest()
        {
            return new[]
            {
                new Subscription
                {
                    Id = 1,
                    ApartmentUrl = "https://example1.com",
                    Email = "user@example.com"
                },
                new Subscription
                {
                    Id = 2,
                    ApartmentUrl = "https://example2.com",
                    Email = "objective@example.com"
                }
            };
        }
    }
}
