using ProAirApiServices.DataLayer.Core.PaymentEngine.Models;

namespace ProAirApiServices.DataLayer.Models.Post
{
    public class MemberRegistrationPost
    {
        public string Email { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Level { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public int? State { get; set; } = null!;
        public int? Zip { get; set; } = null!;
        public bool StoreCreditCard { get; set; } = false;

        public CreditCardModel? CreditCardModel { get; set; }
    }
}
