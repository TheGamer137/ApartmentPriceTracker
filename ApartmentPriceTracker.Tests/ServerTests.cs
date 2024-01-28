using ApartmentPriceTracker.SecondTask;
using FluentAssertions;

namespace ApartmentPriceTracker.Tests
{
    public class ServerTests
    {
        [Fact]
        public void GetCount_ShouldReturnInitialCount()
        {
            // Arrange
            var initialCount = Server.GetCount();

            // Act
            var result = Server.GetCount();

            // Assert
            result.Should().Be(initialCount);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(-3)]
        [InlineData(0)]
        public void AddToCount_ShouldIncreaseCountBySpecifiedValue(int value)
        {
            // Arrange
            var initialCount = Server.GetCount();

            // Act
            Server.AddToCount(value);
            var result = Server.GetCount();

            // Assert
            result.Should().Be(initialCount + value);
        }

        [Fact]
        public void AddToCount_ShouldHandleConcurrentAccess()
        {
            // Arrange
            int threadsCount = 3;
            int expectedTotal = 1 + 2 + 3;
            int initialCount = Server.GetCount();

            // Act
            Parallel.Invoke(
                () => Server.AddToCount(1),
                () => Server.AddToCount(2),
                () => Server.AddToCount(3)
            );

            int result = Server.GetCount();

            // Assert
            result.Should().Be(initialCount + expectedTotal);
        }
    }
}
