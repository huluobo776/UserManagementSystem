using Application.Common;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.Mapping;
using AutoMapper;
using Application.Handlers;
using Application.Queries.GetUser;
using Application.Comands.CreateUserCommand;
using Application.Comands.UpdateUser;
using Application.DTOs.Users;

namespace Application.Service
{
    public class UserService : IUserService //继承自应用层的用户服务接口
    {
        public readonly IUserRepository _repo;
        public readonly IMapper _mapper;
        public readonly ICreateUserHandler _createUserHandler;
        public readonly IGetUserHandler _getUserHandler;
        private readonly  IUpdateUserHandler _updateUserHandler;
        public UserService(IUserRepository repo,IMapper mapper, IGetUserHandler getUserHandler,ICreateUserHandler createUserHandler,IUpdateUserHandler updateUserHandler)
        {
            _repo = repo;
            _mapper = mapper;
            _getUserHandler = getUserHandler;
            _createUserHandler = createUserHandler;
            _updateUserHandler = updateUserHandler;
        }

        /*   appliction层负责
         *   用例(User Case)
         *   业务规则
         *   错误语义(BusinessException)
         *   Dto <-> Entity
         *   调用Repository
         * 
         */
        



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto?> GetByIdAsync(int id) 
            => await _getUserHandler.HandleAsync(new GetUserQuery(id));


        
        //添加用户
        public async Task<int> AddAsync(CreateUserDto userInput)
            =>await  _createUserHandler.HandleAsync(new CreateUserCommand(userInput));
 

        //更新用户
        public async Task<bool> UpdateAsync(UpdateUserDto userInput)
            => await _updateUserHandler.HandleAsync(new UpdateUserCommand(userInput));

        //删除用户
        public async Task<bool> DeleteAsync(int id)
           => await _repo.DeleteAsync(id);


        //获取所有用户
        public async Task<List<UserDto>> GetAllAsync()
        {
            var u = await _repo.GetAllAsync();
            return _mapper.Map<List<UserDto>>(u);
        }
    }
}


