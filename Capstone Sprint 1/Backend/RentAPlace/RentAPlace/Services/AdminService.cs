using RentAPlace.API.DTOs.Property;
using RentAPlace.API.Interfaces;
using RentAPlace.API.Models;

namespace RentAPlace.API.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPropertyService _propertyService;

        public AdminService(IUserRepository userRepo, IPropertyService propertyService)
        {
            _userRepo = userRepo;
            _propertyService = propertyService;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllUsersAsync();
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;

            await _userRepo.DeleteAsync(user);
            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<List<PropertyResponseDto>> GetAllPropertiesAsync()
        {
            return await _propertyService.GetAllPropertiesAsync();
        }
    }
}
