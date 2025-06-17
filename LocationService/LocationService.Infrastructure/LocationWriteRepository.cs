using LocationService.Application;
using LocationService.Domain;
using Microsoft.EntityFrameworkCore;

namespace LocationService.Infrastructure;

public class LocationWriteRepository : ILocationWriteRepository
{
    private readonly LocationDbContext _context;

    public LocationWriteRepository(LocationDbContext context)
    {
        _context = context;
    }

    public async Task<Location> CreateAsync(Location location, CancellationToken cancellationToken = default)
    {
        foreach (var detail in location.Details)
        {
            if (detail.Id == Guid.Empty)
                detail.Id = Guid.NewGuid();
            detail.LocationId = location.Id;
        }

        _context.Locations.Add(location);
        await _context.SaveChangesAsync(cancellationToken);
        
        return location;
    }

    public async Task<Location?> UpdateAsync(Location location, CancellationToken cancellationToken = default)
    {
        var existingLocation = await _context.Locations
            .Include(l => l.Details)
            .FirstOrDefaultAsync(l => l.Id == location.Id, cancellationToken);

        if (existingLocation == null)
            return null;
        
        existingLocation.Address = location.Address;
        existingLocation.Latitude = location.Latitude;
        existingLocation.Longitude = location.Longitude;
        existingLocation.UpdatedAt = DateTime.UtcNow;
        
        _context.LocationDetails.RemoveRange(existingLocation.Details);
        
        foreach (var detail in location.Details)
        {
            if (detail.Id == Guid.Empty)
                detail.Id = Guid.NewGuid();
            detail.LocationId = location.Id;
            existingLocation.Details.Add(detail);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return existingLocation;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var location = await _context.Locations
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

        if (location == null)
            return false;

        _context.Locations.Remove(location);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Locations
            .Include(l => l.Details)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }
}