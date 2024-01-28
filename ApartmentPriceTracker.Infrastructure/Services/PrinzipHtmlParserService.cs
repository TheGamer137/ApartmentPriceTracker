using ApartmentPriceTracker.Api.Services.Interfaces;
using OpenQA.Selenium;

namespace ApartmentPriceTracker.Api.Services
{
    public class PrinzipHtmlParserService : IHtmlParserService
    {
        private readonly IWebDriver _driver;

        public PrinzipHtmlParserService(IWebDriver driver)
        {
            _driver = driver;
        }
        public string GetApartmentPrice(string apartmentUrl)
        {
            try
            {
                _driver.Navigate().GoToUrl(apartmentUrl);
                IWebElement element = _driver.FindElement(By.Id("params_flat_price_raw"));
                return element.GetAttribute("value");
            }
            catch (NoSuchElementException ex)
            {
                throw new Exception($"Элемент с Id 'params_flat_price_raw' не найдет на странице {apartmentUrl}. Ошибка: {ex.Message}");
            }
        }
    }
}
