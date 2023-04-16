using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProAirApiServices.Core;

namespace ProAirApiServices.DataLayer.Core.PaymentEngine
{
    public static class PaymentServices
    {
        public static IServiceScope? ServiceScope;

        public static IPaymentService GetPaymentOption(PaymentMethods paymentType)
        {            
            var instance = paymentType switch {
                PaymentMethods.CreditCard => ServiceScope?.ServiceProvider.GetService<CreditCardStrategy>()
            };

            return instance;
        }
    }
}
