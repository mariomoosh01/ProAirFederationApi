using AuthorizeNet.Api.Contracts.V1;

namespace ProAirApiServices.DataLayer.Core.PaymentEngine.Models
{
    public class CheckoutDto
    {
        public int Amount { get; set; }
        public paymentType? PaymentType { get; set; }
        public creditCardType? CreditCardType { get; set; }
        public customerAddressType? CustomerAddressType { get; set; }
        public lineItemType[]? LineItemTypes { get; set; }
    }
}
