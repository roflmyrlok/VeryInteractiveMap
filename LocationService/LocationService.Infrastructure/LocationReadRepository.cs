using LocationService.Application;
using Microsoft.EntityFrameworkCore;

namespace LocationService.Infrastructure;

public class LocationReadRepository : ILocationReadRepository
{
    private readonly LocationDbContext _context;

    public LocationReadRepository(LocationDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Locations
            .Include(l => l.Details)
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Domain.Location>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var skip = (pageNumber - 1) * pageSize;
        
        return await _context.Locations
            .Include(l => l.Details)
            .AsNoTracking()
            .OrderBy(l => l.CreatedAt)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Locations.CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Domain.Location>> SearchAsync(
        string query, 
        double? latitude, 
        double? longitude, 
        double? radiusKm, 
        int pageNumber, 
        int pageSize, 
        CancellationToken cancellationToken = default)
    {
        var skip = (pageNumber - 1) * pageSize;
        var queryable = _context.Locations
            .Include(l => l.Details)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            var searchTerm = $"%{query.ToLower()}%";
            queryable = queryable.Where(l => 
                EF.Functions.Like(l.Address.ToLower(), searchTerm) ||
                l.Details.Any(d => EF.Functions.Like(d.PropertyValue.ToLower(), searchTerm)));
        }

        if (latitude.HasValue && longitude.HasValue && radiusKm.HasValue)
        {
            var lat = latitude.Value;
            var lng = longitude.Value;
            var radius = radiusKm.Value;

            queryable = queryable.Where(l => 
                (6371 * Math.Acos(
                    Math.Cos(Math.PI * lat / 180) * 
                    Math.Cos(Math.PI * l.Latitude / 180) * 
                    Math.Cos(Math.PI * l.Longitude / 180 - Math.PI * lng / 180) + 
                    Math.Sin(Math.PI * lat / 180) * 
                    Math.Sin(Math.PI * l.Latitude / 180)
                )) <= radius);
        }

        return await queryable
            .OrderBy(l => l.CreatedAt)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetSearchCountAsync(
        string query, 
        double? latitude, 
        double? longitude, 
        double? radiusKm, 
        CancellationToken cancellationToken = default)
    {
        var queryable = _context.Locations.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(query))
        {
            var searchTerm = $"%{query.ToLower()}%";
            queryable = queryable.Where(l => 
                EF.Functions.Like(l.Address.ToLower(), searchTerm) ||
                l.Details.Any(d => EF.Functions.Like(d.PropertyValue.ToLower(), searchTerm)));
        }

        if (latitude.HasValue && longitude.HasValue && radiusKm.HasValue)
        {
            var lat = latitude.Value;
            var lng = longitude.Value;
            var radius = radiusKm.Value;

            queryable = queryable.Where(l => 
                (6371 * Math.Acos(
                    Math.Cos(Math.PI * lat / 180) * 
                    Math.Cos(Math.PI * l.Latitude / 180) * 
                    Math.Cos(Math.PI * l.Longitude / 180 - Math.PI * lng / 180) + 
                    Math.Sin(Math.PI * lat / 180) * 
                    Math.Sin(Math.PI * l.Latitude / 180)
                )) <= radius);
        }

        return await queryable.CountAsync(cancellationToken);
    }
}