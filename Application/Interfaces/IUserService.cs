using Application.Common;
using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<User?>> GetByIdAsync(int id);

        Task<Result> AddAsync(userInput userInput);

        Task<Result> UpdateAsync(userInput user);

        Task<Result> DeleteAsync(int id);

        Task<Result<List<User>>> GetAllAsync();
    }
}
