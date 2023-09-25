using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourPackage.Models.Context;
using TourPackage.Interfaces;
using TourPackage.Exceptions;

namespace TourPackage.Models
{
    public class InclusionsRepo : IRepo<Inclusions,int>
    {
        private readonly TourContext _context;

        public InclusionsRepo(TourContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Inclusions>> GetAll()
        {
            if (_context.Inclusions != null)
            {
                return await _context.Inclusions.ToListAsync();
            }
            else
            {
               
                return new List<Inclusions>();
            }
        }

        public async Task<Inclusions> Get(int id)
        {
            if (_context.Inclusions != null)
            {
                return await _context.Inclusions.FindAsync(id);
            }
            else
            {

                throw new DatabaseEmptyException("Database is empty");
            }
        }

        public async Task<Inclusions> Add(Inclusions inclusion)
        {
            if (_context.Inclusions != null)
            {
                _context.Inclusions.Add(inclusion);
                await _context.SaveChangesAsync();
                return inclusion;
            }
            else
            {
              
                throw new DatabaseEmptyException("Database is empty");
            }
        }

        public async Task<Inclusions> Update(Inclusions inclusion)
        {
            if (_context.Inclusions != null)
            {
                var existingInclusion = _context.Inclusions.FirstOrDefault(u => u.InclusionId == inclusion.InclusionId);
                if (existingInclusion != null)
                {
                    existingInclusion.InclusionId = inclusion.InclusionId;
                    existingInclusion.InclusionDescriptionn = inclusion.InclusionDescriptionn;
                    await _context.SaveChangesAsync();
                    return inclusion;
                }
                else
                {
                   
                    throw new InclusionNotFoundException("Inclusion not found.");
                }
            }
            else
            {
              
                throw new DatabaseEmptyException("Database is empty");
            }
        }

        public async Task<Inclusions> Delete(int id)
        {
            if (_context.Inclusions != null)
            {
                var inclusion = await _context.Inclusions.FindAsync(id);
                if (inclusion != null)
                {
                    _context.Inclusions.Remove(inclusion);
                    await _context.SaveChangesAsync();
                    return inclusion;
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
