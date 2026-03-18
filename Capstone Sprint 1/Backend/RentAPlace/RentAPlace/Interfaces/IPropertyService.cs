using RentAPlace.API.DTOs.Property;
using Microsoft.AspNetCore.Http;

namespace RentAPlace.API.Interfaces
{
    public interface IPropertyService
    {
        Task<PropertyResponseDto> CreatePropertyAsync(CreatePropertyDto dto, int ownerId, List<IFormFile>? images);
        Task<PropertyResponseDto?> UpdatePropertyAsync(int propertyId, UpdatePropertyDto dto, int ownerId);
        Task<bool> DeletePropertyAsync(int propertyId, int ownerId);
        Task<List<PropertyResponseDto>> GetOwnerPropertiesAsync(int ownerId);
        Task<List<PropertyResponseDto>> SearchPropertiesAsync(string? location, string? propertyType, string? features, DateTime? checkIn, DateTime? checkOut);
        Task<List<PropertyResponseDto>> GetAllPropertiesAsync();
        Task<PropertyResponseDto?> GetPropertyByIdAsync(int propertyId);
    }
}
