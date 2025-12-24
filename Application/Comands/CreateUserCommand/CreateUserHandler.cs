using Application.Common;
using Application.Exceptions;
using Application.Handlers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Comands.CreateUserCommand
{
    public class CreateUserHandler : ICreateUserHandler
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public CreateUserHandler(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        ///
        public async Task<int> HandleAsync(CreateUserCommand command)
        {
            var dto = command.Dto;

            //幂等 /唯一性校验
            var exists = await _repo.GetByEmailAsync(dto.Email);
            if (exists != null)
                throw new BusinessException(ErrorCode.UserAlreadyExists, "邮箱已被注册");


            var user = new User(dto.Name, dto.Email, dto.Password);
            await _repo.AddAsync(user);
            //假设Addaync会设置user》id(EF Core 在SaveChanges后会)

            return user.Id;
        }



    }
}
