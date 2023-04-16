using Microsoft.Extensions.Configuration;
using ProAirApiServices.DataLayer.Core.PaymentEngine.Models;

namespace ProAirApiServices.DataLayer.Core.PaymentEngine
{
    public class DebitCardStrategy : IPaymentService<DebitCardModel>
    {
        public DebitCardStrategy(IConfiguration config)
        {

        }

        public bool Checkout(DebitCardModel paymentMethod)
        {
            throw new NotImplementedException();
        }
    }
}
