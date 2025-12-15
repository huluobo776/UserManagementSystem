using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Service
{
    public class UserService : IUserService //继承自应用层的用户服务接口
    {
        public readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }


        public async Task<Result<User?>> GetByIdAsync(int id)
        {

            var u = await _repo.GetByIdAsync(id);
            if (u == null)
            {
                return Result<User?>.Fail("用户不存在");
            }
            return Result<User?>.DataResult(u);
        }



        //添加用户
        public async Task<Result> AddAsync(userInput userInput)
        {
            //以后这里会放业务规则
            var user = new User(userInput.Name, userInput.Email, userInput.Password);
            await _repo.AddAsync(user);
            return Result.Ok();
        }

        //更新用户
        public async Task<Result> UpdateAsync(userInput userInput)
        {
            //这里后续补充业务规则
            var user = new User(userInput.Name, userInput.Email, userInput.Password);
            await _repo.UpdateAsync(user);
            return Result.Ok();
        }

        //删除用户
        public async Task<Result> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return Result.Ok();
        }

        //获取所有用户
        public async Task<Result<List<User>>> GetAllAsync()
        {
            var u = await _repo.GetAllAsync();
            return Result<List<User>>.DataResult(u);
        }
    }
}


