using AuthorizeNet.Api.Contracts.V1;
using Microsoft.Extensions.Configuration;
using ProAirApiServices.DataLayer.Core.PaymentEngine.Gateways;
using ProAirApiServices.DataLayer.Core.PaymentEngine.Models;

namespace ProAirApiServices.DataLayer.Core.PaymentEngine
{
    public class CreditCardStrategy: AuthorizeNetProcessor, IPaymentService<CreditCardModel>
    {
        public CreditCardStrategy(IConfiguration config):base(config)
        {

        }

        public bool Checkout(CreditCardModel model)
        {            
            var checkoutModel = new CheckoutDto
            {
                Amount = model.Total,
                CustomerAddressType = new customerAddressType { firstName = model.FirstName, lastName = model.LastName, address = model.Address, city = model.City, zip = model.Zip.ToString() },
                CreditCardType = new creditCardType { cardNumber = model.CardNumber, expirationDate = model.ExpirationDate, cardCode = model.CardCode.ToString() }
            };

            checkoutModel.PaymentType = new paymentType { Item = checkoutModel.CreditCardType };

            var response = ChargeCreditCard(checkoutModel);

            return response?.messages.resultCode == messageTypeEnum.Ok;
        }       
    }
}
