using BookingService.Interfaces;
using BookingService.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookingService.Services
{
    public class ManagePassengerService : IPassengerRepo
    {
        private readonly IRepo<int, Passenger> _passengerRepo;
        private readonly ILogger<ManagePassengerService> _logger;

        public ManagePassengerService(IRepo<int, Passenger> passengerRepo,ILogger<ManagePassengerService> logger)
        {
            _passengerRepo = passengerRepo;
            _logger = logger;
        }
        public async Task<Passenger?> AddPassenger(Passenger passenger)
        {
            try
            {
                await _passengerRepo.Add(passenger);
                return passenger;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Passenger?> DeletePassenger(int id)
        {
            try
            {
                var passenger = await GetPassenger(id);
                if (passenger!=null)
                {
                    await _passengerRepo.Delete(passenger);
                    return passenger;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<List<Passenger>?> GetAllPassenger()
        {
            try
            {
                var passengers = await _passengerRepo.GetAll();
                if (passengers != null)
                {
                    return passengers;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Passenger?> GetPassenger(int id)
        {
            try
            {
                var passenger = await _passengerRepo.Get(id);
                if (passenger != null)
                {
                    return passenger;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Passenger?> UpdatePassenger(Passenger passenger)
        {
            try
            {
                var updatingPassenger = await _passengerRepo.Get(passenger.PassengerId);
                if (updatingPassenger != null)
                {
                    updatingPassenger.PassengerId = passenger.PassengerId;
                    updatingPassenger.Name = passenger.Name;
                    updatingPassenger.Gender = passenger.Gender;
                    updatingPassenger.Age = passenger.Age;
                    updatingPassenger.BookingId = passenger.BookingId;
                    await _passengerRepo.Update(updatingPassenger);
                    return passenger;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
