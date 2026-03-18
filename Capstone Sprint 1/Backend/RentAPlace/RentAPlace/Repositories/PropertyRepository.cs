using Microsoft.EntityFrameworkCore;
using RentAPlace.API.Data;
using RentAPlace.API.Interfaces;
using RentAPlace.API.Models;

namespace RentAPlace.API.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _db;

        public PropertyRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Property>> GetAllAsync()
        {
            return await _db.Properties
                .Include(p => p.Owner)
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<Property?> GetByIdAsync(int propertyId)
        {
            return await _db.Properties
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.PropertyId == propertyId);
        }

        public async Task<List<Property>> GetByOwnerIdAsync(int ownerId)
        {
            return await _db.Properties
                .Include(p => p.Owner)
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<List<Property>> SearchAsync(string? location, string? propertyType, string? features)
        {
            var query = _db.Properties
                .Include(p => p.Owner)
                .Where(p => p.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(p => p.Location.Contains(location));

            if (!string.IsNullOrWhiteSpace(propertyType))
                query = query.Where(p => p.PropertyType == propertyType);

            if (!string.IsNullOrWhiteSpace(features))
                query = query.Where(p => p.Features != null && p.Features.Contains(features));

            return await query.ToListAsync();
        }

        public async Task AddAsync(Property property)
        {
            await _db.Properties.AddAsync(property);
        }

        public async Task UpdateAsync(Property property)
        {
            _db.Properties.Update(property);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Property property)
        {
            _db.Properties.Remove(property);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
