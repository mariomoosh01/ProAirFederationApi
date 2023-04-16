
namespace ProAirApiServices.DataLayer.Models.Dto.Login
{
    public class MemberDto
    {
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; }
        public int? State { get; set; }
        public short? Zip { get; set; }
        public int? MembershipLevel { get; set; }
    }
}
