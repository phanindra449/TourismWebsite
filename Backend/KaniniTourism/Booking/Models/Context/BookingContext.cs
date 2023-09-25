using Microsoft.EntityFrameworkCore;

namespace BookingService.Models.Context
{
    public class BookingContext:DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options):base(options)
        {
            
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
    }
}
