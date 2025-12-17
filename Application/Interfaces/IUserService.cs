using Application.Common;
using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(int id);

        Task<int> AddAsync(CreateUserDto userInput);

        Task<bool> UpdateAsync(userInput user);

        Task<bool> DeleteAsync(int id);

        Task<List<UserDto>> GetAllAsync();
    }
}
