using Microsoft.EntityFrameworkCore;
using ProAirApiServices.DataLayer.DataAccess.Entities;

namespace ProAirApiServices.DataLayer.DataAccess.Core
{
    public class ProAirDbContext: DbContext
    {
        #region Properties

        internal virtual DbSet<Members> Members { get; set; }
        internal virtual DbSet<States> States { get; set; }
        internal virtual DbSet<MembersCreditCards> MemberCreditCards { get; set; }

        #endregion

        public ProAirDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Members>()
                .HasKey(pk => pk.Email);

            modelBuilder.Entity<States>()
                .HasKey(pk => pk.Id);

            modelBuilder.Entity<MembersCreditCards>()
                .HasKey(pk => pk.Id);
        }
    }
}
