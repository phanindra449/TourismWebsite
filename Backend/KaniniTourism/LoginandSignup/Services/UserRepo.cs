using LoginandSignup.Models;
using LoginandSignup.Interfaces;
using LoginandSignup.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace LoginandSignup.Services
{
    public class UserRepo : IRepo<User, int>
    {
        private readonly UserContext _userContext;

        public UserRepo(UserContext employeeContext)
        {
            _userContext = employeeContext;
        }
        public async Task<User?> Add(User user)
        {
            if (_userContext.Users != null)
            {
                _userContext.Users.Add(user);
                await _userContext.SaveChangesAsync();
                return user;
            }
            throw new Exception("Database is empty");
        }

        public async Task<User?> Delete(int key)
        {
            if (_userContext.Users != null)
            {
                var user = await Get(key);
                if (user != null)
                {
                    _userContext.Users.Remove(user);
                    await _userContext.SaveChangesAsync();
                    return user;
                }
                return null;
            }
            throw new Exception("Database is empty");
        }

        public async Task<User?> Get(int key)
        {
            if (_userContext.Users != null)
            {
                var user = await _userContext.Users.FirstOrDefaultAsync(u => u.UserId == key);
                return user;
            }

            throw new Exception("Database is empty");
        }

        public async Task<ICollection<User>?> GetAll()
        {
            if (_userContext.Users != null)
            {
                var users = await _userContext.Users.ToListAsync();
                return users;
            }
            throw new Exception("Database is empty");
        }

        public async Task<User?> Update(User user)
        {
            if (_userContext.Users != null)
            {
                var userDetails = await Get(user.UserId);
                if (userDetails != null)
                {
                    userDetails.Role = user.Role;
                    userDetails.PasswordKey = user.PasswordKey;
                    userDetails.PasswordHash = user.PasswordHash;
                    await _userContext.SaveChangesAsync();
                    return user;
                }
                return null;
            }
            throw new Exception("Database is empty");
        }
    }

}
