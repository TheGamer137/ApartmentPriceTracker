using ApartmentPriceTracker.Api.Services;
using FakeItEasy;
using FluentAssertions;
using OpenQA.Selenium;

namespace ApartmentPriceTracker.Tests.Services
{
    public class PrinzipHtmlParserServiceTests
    {
        private readonly IWebDriver _driver;
        public PrinzipHtmlParserServiceTests()
        {
            _driver = A.Fake<IWebDriver>();
        }
        [Fact]
        public void GetApartmentPrice_ValidPrice_ReturnsParsedValue()
        {
            // Arrange
            var fakeElement = A.Fake<IWebElement>();
            A.CallTo(() => _driver.FindElement(A<By>._)).Returns(fakeElement);
            A.CallTo(() => fakeElement.GetAttribute("value")).Returns("8559000");
            var htmlParserService = new PrinzipHtmlParserService(_driver);

            // Act
            var result = htmlParserService.GetApartmentPrice("https://example.com");

            // Assert
            result.Should().Be("8559000");
        }

        [Fact]
        public void GetApartmentPrice_ElementNotFound_ThrowsException()
        {
            // Arrange
            A.CallTo(() => _driver.FindElement(By.Id("params_flat_price_raw"))).Throws<NoSuchElementException>();
            var parserService = new PrinzipHtmlParserService(_driver);

            // Act & Assert
            parserService.Invoking(p => p.GetApartmentPrice("https://example.com")).Should().Throw<Exception>();
        }
    }
}
