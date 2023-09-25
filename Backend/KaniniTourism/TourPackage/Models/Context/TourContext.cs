using Microsoft.EntityFrameworkCore;

namespace TourPackage.Models.Context
{
    public class TourContext : DbContext
    {



        public TourContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<TourDetails> TourDetails { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<TourDestination> TourDestinations { get; set; }
        public DbSet<TourDate> TourDates { get; set; }
        public DbSet<TourInclusion> TourInclusions { get; set; }
        public DbSet<TourExclusion> TourExclusions { get; set; }
        public DbSet<Exclusions> Exclusions { get; set; }
        public DbSet<Inclusions> Inclusions { get; set; }






    }
}
