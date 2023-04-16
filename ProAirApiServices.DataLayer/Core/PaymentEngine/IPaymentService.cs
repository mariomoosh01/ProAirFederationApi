namespace ProAirApiServices.DataLayer.Core.PaymentEngine
{
    public interface IPaymentService 
    {
    
    }
    public interface IPaymentService<T> : IPaymentService where T : IPaymentModel
    {
        bool Checkout(T paymentMethod);
    }
}
