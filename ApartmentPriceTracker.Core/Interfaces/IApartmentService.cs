using ApartmentPriceTracker.Core.DTOs;

namespace ApartmentPriceTracker.Api.Services.Interfaces
{
    public interface IApartmentService
    {
        /// <summary>
        /// Метод для подписки на изменение цены
        /// </summary>
        /// <param name="url">Ссылка на объявление</param>
        /// <param name="email">Email на который присылать уведомления</param>
        Task SaveSubscriptionAsync(string url, string email);

        /// <summary>
        /// Метод, который вернет актуальные цены квартир, для которых выполнена подписка, и ссылки на эти квартиры
        /// </summary>
        /// <returns>Цены квартир и ссылки на них</returns>
        IEnumerable<Apartment> GetApartments();
    }
}
