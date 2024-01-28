namespace ApartmentPriceTracker.Api.Services.Interfaces
{
    public interface IHtmlParserService
    {
        string GetApartmentPrice(string apartmentUrl);
    }
}
