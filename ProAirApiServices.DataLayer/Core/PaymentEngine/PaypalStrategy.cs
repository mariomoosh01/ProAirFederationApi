using ProAirApiServices.DataLayer.Core.PaymentEngine.Models;

namespace ProAirApiServices.DataLayer.Core.PaymentEngine
{
    internal class PaypalStrategy : IPaymentService<PaypalModel>
    {       
        public bool Checkout(PaypalModel paymentMethod)
        {
            throw new NotImplementedException();
        }
    }
}
