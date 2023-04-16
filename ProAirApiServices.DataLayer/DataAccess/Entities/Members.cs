using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProAirApiServices.DataLayer.DataAccess.Entities
{
    [Table("Members", Schema ="dbo")]
    internal class Members
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required] 
        public string FirstName { get; set; } = null!;
        [Required] 
        public string LastName { get; set; } = null!;
        [Required] 
        public string DisplayName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public int? State { get; set; }
        public short? Zip { get; set; }
        public short? MembershipLevel { get; set; }
        public DateTime ExpirationDate { get; set; }
        [Required] 
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
        
    }
}
