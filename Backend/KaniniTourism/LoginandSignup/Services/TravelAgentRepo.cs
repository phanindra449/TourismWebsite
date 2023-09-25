using LoginandSignup.Models;
using LoginandSignup.Models.Context;
using Microsoft.EntityFrameworkCore;
using LoginandSignup.Interfaces;

namespace LoginandSignup.Services
{
    public class TravelAgentRepo : IRepo<TravelAgent,int>
    {
        private readonly UserContext _context;
        private readonly ILogger<User> _logger;

        public TravelAgentRepo(UserContext context, ILogger<User> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TravelAgent?> Add(TravelAgent item)
        {
            try
            {
                _context.TravelAgents.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<TravelAgent?> Delete(int key)
        {
            try
            {
                var travelagent = await Get(key);
                if (travelagent != null)
                {
                    _context.TravelAgents.Remove(travelagent);
                    await _context.SaveChangesAsync();
                    return travelagent;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<TravelAgent?> Get(int key)
        {
            try
            {
                var travelagent = await _context.TravelAgents.Include(i => i.Users).FirstOrDefaultAsync(i => i.TravelAgentId == key);
                return travelagent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<TravelAgent>>GetAll()
        {
            try
            {
                var travelagent = await _context.TravelAgents.Include(d => d.Users).ToListAsync();
                if (travelagent.Count > 0)
                    return travelagent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<TravelAgent?> Update(TravelAgent item)
        {
            try
            {
                var travelagent = _context.TravelAgents.FirstOrDefault(u => u.TravelAgentId == item.TravelAgentId); ;
                if (travelagent != null)
                {
                    travelagent.Username = item.Username;
                    travelagent.Email = item.Email;
                    travelagent.ContactNumber = item.ContactNumber;
                    travelagent.Address = item.Address;
                    travelagent.LicenseNumber = item.LicenseNumber;
                    travelagent.YearsInBusiness=item.YearsInBusiness;
                    travelagent.AgencyName = item.AgencyName;
                    travelagent.Country = item.Country;
                    travelagent.LicenseExpiryDate = item.LicenseExpiryDate;
                    travelagent.IsActive = item.IsActive;



                    await _context.SaveChangesAsync();
                    return travelagent;
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
