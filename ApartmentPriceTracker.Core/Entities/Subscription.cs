using ApartmentPriceTracker.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace ApartmentPriceTracker.Api.Models
{
    public class Subscription : BaseEntity
    {
        [Required]
        [DataType(DataType.Url)]
        public string ApartmentUrl { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
