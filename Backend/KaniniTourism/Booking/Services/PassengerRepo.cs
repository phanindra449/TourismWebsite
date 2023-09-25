using BookingService.Interfaces;
using BookingService.Models;
using BookingService.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Services
{
    public class PassengerRepo : IRepo<int, Passenger>
    {
        private BookingContext _context;
        private readonly ILogger<PassengerRepo> _logger;

        public PassengerRepo(BookingContext context, ILogger<PassengerRepo> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Passenger?> Add(Passenger item)
        {
            try
            {
                await _context.Passengers.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Passenger?> Delete(Passenger item)
        {
            try
            {
                var passenger = await Get(item.PassengerId);
                if (passenger != null)
                {
                    _context.Passengers.Remove(passenger);
                    await _context.SaveChangesAsync();
                    return passenger;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Passenger?> Get(int key)
        {
            try
            {
                return await _context.Passengers.FirstOrDefaultAsync(p=>p.PassengerId==key);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<List<Passenger>?> GetAll()
        {
            try
            {
                return await _context.Passengers.ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Passenger?> Update(Passenger item)
        {
            try
            {
                _context.Passengers.Update(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
