using LoginandSignup.Interfaces;
using LoginandSignup.Models;
using LoginandSignup.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginandSignup.Services
{
    public class AdminRepo : IRepo<Admin, int>
    {
        private readonly UserContext _context;
        private readonly ILogger<AdminRepo> _logger;

        public AdminRepo(UserContext hospitalContext, ILogger<AdminRepo> logger)
        {
            _context = hospitalContext;
            _logger = logger;
        }

        public async Task<Admin?> Add(Admin item)
        {
            try
            {
                _context.Admins.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Admin?> Get(int key)
        {
            try
            {
                var user = await _context.Admins.FirstOrDefaultAsync(u => u.AdminId == key);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Admin>?> GetAll()
        {
            try
            {
                var users = await _context.Admins.ToListAsync();
                if (users.Count > 0)
                    return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Admin?> Update(Admin item)
        {
            try
            {
                var existingAdmin = await _context.Admins.FirstOrDefaultAsync(u => u.AdminId == item.AdminId);
                if (existingAdmin != null)
                {
                    existingAdmin.Name = item.Name;
                    existingAdmin.Email = item.Email;
                    existingAdmin.PhoneNumber = item.PhoneNumber;

                    await _context.SaveChangesAsync();
                    return existingAdmin;
                }
                return null; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Admin?> Delete(int key)
        {
            try
            {
                var existingAdmin = await _context.Admins.FirstOrDefaultAsync(u => u.AdminId == key);
                if (existingAdmin != null)
                {
                    _context.Admins.Remove(existingAdmin);
                    await _context.SaveChangesAsync();
                    return existingAdmin;
                }
                return null; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
