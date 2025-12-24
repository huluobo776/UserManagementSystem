using Application.Common;
using Application.DTOs.Users;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(int id);

        Task<int> AddAsync(CreateUserDto userInput);

        Task<bool> UpdateAsync(UpdateUserDto user);

        Task<bool> DeleteAsync(int id);

        Task<List<UserDto>> GetAllAsync();
    }
}
