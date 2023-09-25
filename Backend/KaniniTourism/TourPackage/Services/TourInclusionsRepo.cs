using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourPackage.Models;
using TourPackage.Models.Context;
using TourPackage.Interfaces;
using TourPackage.Exceptions;

public class TourInclusionRepo : IRepo<TourInclusion, int>
{
    private readonly TourContext _context;

    public TourInclusionRepo(TourContext context)
    {
        _context = context;
    }

    public async Task<TourInclusion> Add(TourInclusion tourInclusion)
    {
        if (tourInclusion == null)
        {
            throw new ArgumentNullException("TourInclusion must not be null.");
        }

        _context.TourInclusions.Add(tourInclusion);
        await _context.SaveChangesAsync();

        return tourInclusion;
    }

    public async Task<TourInclusion> Get(int id)
    {
        return await _context.TourInclusions.FirstOrDefaultAsync(ti => ti.Id == id);
    }

    public async Task<ICollection<TourInclusion>> GetAll()
    {
        return await _context.TourInclusions.ToListAsync();
    }

    public async Task<TourInclusion> Update(TourInclusion tourInclusion)
    {
        var existingInclusion = await _context.TourInclusions.FirstOrDefaultAsync(ti => ti.Id == tourInclusion.Id);

        if (existingInclusion == null)
        {
            throw new ArgumentException("TourInclusion not found.");
        }

        existingInclusion.InclusionId = tourInclusion.InclusionId;
        existingInclusion.TourId = tourInclusion.TourId;

        await _context.SaveChangesAsync();
        return existingInclusion;
    }

    public async Task<TourInclusion> Delete(int id)
    {
        var tourInclusion = await _context.TourInclusions.FirstOrDefaultAsync(ti => ti.Id == id);

        if (tourInclusion == null)
        {
            throw new ArgumentException("TourInclusion not found.");
        }

        _context.TourInclusions.Remove(tourInclusion);
        await _context.SaveChangesAsync();
        return tourInclusion;
    }
}
