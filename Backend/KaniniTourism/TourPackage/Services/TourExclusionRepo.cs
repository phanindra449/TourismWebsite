using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourPackage.Models;
using TourPackage.Models.Context;
using TourPackage.Interfaces;
using TourPackage.Exceptions;

public class TourExclusionRepo : IRepo<TourExclusion, int>
{
    private readonly TourContext _context;

    public TourExclusionRepo(TourContext context)
    {
        _context = context;
    }

    public async Task<TourExclusion> Add(TourExclusion tourExclusion)
    {
        if (tourExclusion == null)
        {
            throw new ArgumentNullException("TourExclusion must not be null.");
        }

        _context.TourExclusions.Add(tourExclusion);
        await _context.SaveChangesAsync();

        return tourExclusion;
    }

    public async Task<TourExclusion> Get(int id)
    {
        
        return await _context.TourExclusions.FirstOrDefaultAsync(te => te.Id == id);
    }

    public async Task<ICollection<TourExclusion>> GetAll()
    {
        return await _context.TourExclusions.ToListAsync();
    }

    public async Task<TourExclusion> Update(TourExclusion tourExclusion)
    {
        var existingExclusion = await _context.TourExclusions.FirstOrDefaultAsync(te => te.Id == tourExclusion.Id);

        if (existingExclusion == null)
        {
            throw new ArgumentException("TourExclusion not found.");
        }

        existingExclusion.ExclusionId = tourExclusion.ExclusionId;
        existingExclusion.TourId = tourExclusion.TourId;

        await _context.SaveChangesAsync();
        return existingExclusion;
    }

    public async Task<TourExclusion> Delete(int id)
    {
        var tourExclusion = await _context.TourExclusions.FirstOrDefaultAsync(te => te.Id == id);

        if (tourExclusion == null)
        {
            throw new ArgumentException("TourExclusion not found.");
        }

        _context.TourExclusions.Remove(tourExclusion);
        await _context.SaveChangesAsync();
        return tourExclusion;
    }
}
