using ProAirApiServices.Core;

namespace ProAirApiServices.DataLayer.Models.Dto.Subscriptions
{
    public class CreditCardCheckoutDto
    {
        public int Id { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        public int Total { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpirationDate { get; set; }
        public string? CardCode { get; set; }

    }
}
