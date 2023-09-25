using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourPackage.Models.Context;
using TourPackage.Interfaces;
using TourPackage.Exceptions;
using Microsoft.VisualBasic;

namespace TourPackage.Models
{
    public class ExclusionsRepo : IRepo<Exclusions,int>
    {
        private readonly TourContext _context;

        public ExclusionsRepo(TourContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Exclusions>> GetAll()
        {
            if (_context.Exclusions != null)
            {
                return await _context.Exclusions.ToListAsync();
            }
            else
            {
                return new List<Exclusions>();
            }
        }

        public async Task<Exclusions?> Get(int id)
        {
            if (_context.Exclusions != null)
            {
                var exclusion = _context.Exclusions.FirstOrDefault(u=>u.ExclusionId==id);
                if (exclusion != null)
                {
                    return exclusion;

                }
                else
                {
                    throw new ExclusionNotFoundException("exclusion not found");


                }
            }
            else
            {
                throw new DatabaseEmptyException("Database is empty");
            }
        }

        public async Task<Exclusions> Add(Exclusions exclusion)
        {
            if (_context.Exclusions != null)
            {
                _context.Exclusions.Add(exclusion);
                await _context.SaveChangesAsync();
                return exclusion;
            }
            else
            {
                throw new DatabaseEmptyException("Database is empty");
            }
        }

        public async Task<Exclusions> Update(Exclusions exclusion)
        {
            if (_context.Exclusions != null)
            {
                var existingExclusion = _context.Exclusions.FirstOrDefault(u => u.ExclusionId == exclusion.ExclusionId);
                if (existingExclusion != null)
                {
                    existingExclusion.ExclusionId = exclusion.ExclusionId;
                    existingExclusion.ExclusionDescription = exclusion.ExclusionDescription;
                    await _context.SaveChangesAsync();
                    return exclusion;
                }
                else
                {
                    throw new ExclusionNotFoundException("Exclusion not found.");
                }
            }
            else
            {
                throw new DatabaseEmptyException("Database is empty");
            }
        }

        public async Task<Exclusions> Delete(int id)
        {
            if (_context.Exclusions != null)
            {
                var exclusion = await _context.Exclusions.FindAsync(id);
                if (exclusion != null)
                {
                    _context.Exclusions.Remove(exclusion);
                    await _context.SaveChangesAsync();
                    return exclusion;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new DatabaseEmptyException("Database is empty");
            }
        }
    }
}
