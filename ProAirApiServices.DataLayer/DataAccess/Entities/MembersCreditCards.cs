using System.ComponentModel.DataAnnotations.Schema;

namespace ProAirApiServices.DataLayer.DataAccess.Entities
{
    [Table("MembersCreditCards",Schema = "dbo")]
    internal class MembersCreditCards
    {
        public int Id { get; set; }
        public string MemberId { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public string ExpirationDate { get; set; } = null!;
        public int CvvCode { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool UseProfileAddress { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public int? StateId { get; set; }
        public short? Zip { get; set; } = null!;
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
    }
}
