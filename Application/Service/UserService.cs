using Application.Common;
using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.Mapping;
using AutoMapper;

namespace Application.Service
{
    public class UserService : IUserService //继承自应用层的用户服务接口
    {
        public readonly IUserRepository _repo;
        public readonly IMapper _mapper;
        public UserService(IUserRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var u = await _repo.GetByIdAsync(id);
            if (u == null)
            {
                throw new BusinessException(ErrorCode.UserNotFound);//抛出业务异常
            }

            return _mapper.Map<UserDto>(u);
        }



        //添加用户
        public async Task<int> AddAsync(CreateUserDto userInput)
        {
            //以后这里会放业务规则
            var user = new User(userInput.Name, userInput.Email, userInput.Password);
            var userid = await _repo.AddAsync(user);
            return userid;
        }

        //更新用户
        public async Task<bool> UpdateAsync(userInput userInput)
        {
            //这里后续补充业务规则
            var user = new User(userInput.Name, userInput.Email, userInput.Password);
            await _repo.UpdateAsync(user);
            return true;
        }

        //删除用户
        public async Task<bool> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }

        //获取所有用户
        public async Task<List<UserDto>> GetAllAsync()
        {
            var u = await _repo.GetAllAsync();
            return _mapper.Map<List<UserDto>>(u);
        }
    }
}


