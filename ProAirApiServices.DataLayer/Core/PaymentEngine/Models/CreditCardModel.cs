namespace ProAirApiServices.DataLayer.Core.PaymentEngine.Models
{
    public class CreditCardModel: IPaymentModel
    {
        public int Id { get; set; }
        public int Total { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool UseProfileAddress { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public int? State { get; set; } = null!;
        public int? Zip { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public string ExpirationDate { get; set; } = null!;
        public short CardCode { get; set; }
    }
}
