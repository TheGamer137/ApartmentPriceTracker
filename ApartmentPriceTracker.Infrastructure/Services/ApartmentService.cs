using ApartmentPriceTracker.Api.Models;
using ApartmentPriceTracker.Api.Services.Interfaces;
using ApartmentPriceTracker.Core.DTOs;

namespace ApartmentPriceTracker.Api.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly AppDbContext _context;
        private readonly IHtmlParserService _htmlParserService;

        public ApartmentService(AppDbContext context, IHtmlParserService htmlParserService)
        {
            _context = context;
            _htmlParserService = htmlParserService;
        }

        public IEnumerable<Apartment> GetApartments()
        {
            var urls = _context.Subscriptions.Select(s => s.ApartmentUrl);
            var apartments = new List<Apartment>();

            foreach (var url in urls)
            {
                var price = _htmlParserService.GetApartmentPrice(url);
                apartments.Add(new Apartment { ApartmentUrl = url, Price = price });
            }

            return apartments;
        }

        public async Task SaveSubscriptionAsync(string url, string email)
        {
            var existingSubscription = GetSubscriptionByEmailAndUrl(url, email);

            if (existingSubscription != null)
            {
                throw new InvalidOperationException($"Подписка по {email} на объявлению {url} уже оформлена");
            }

            var subscription = new Subscription
            {
                ApartmentUrl = url,
                Email = email
            };
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
        }
        private Subscription? GetSubscriptionByEmailAndUrl(string url, string email) =>
            _context.Subscriptions.FirstOrDefault(s => s.ApartmentUrl == url && s.Email == email);
    }
}
