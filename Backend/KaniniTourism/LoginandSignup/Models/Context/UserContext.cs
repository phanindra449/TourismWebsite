using Microsoft.EntityFrameworkCore;

namespace LoginandSignup.Models.Context
{
    public class UserContext : DbContext
    {

        public UserContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }  

        public DbSet<TravelAgent> TravelAgents { get; set; }
        public DbSet<Traveller> Travellers { get; set; }
        public DbSet<Admin> Admins { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TravelAgent>().Property(i => i.TravelAgentId).ValueGeneratedNever();
            modelBuilder.Entity<Traveller>().Property(i => i.TravellerId).ValueGeneratedNever();
            modelBuilder.Entity<Admin>().Property(i => i.AdminId).ValueGeneratedNever();
        }



    }
}
