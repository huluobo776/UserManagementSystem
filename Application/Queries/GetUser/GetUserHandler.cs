using AutoMapper;
using Domain.Interfaces;
using Application.Exceptions;
using Application.Handlers;
using Application.DTOs.Users;

namespace Application.Queries.GetUser
{
    public class GetUserHandler : IGetUserHandler
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public GetUserHandler(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserDto> HandleAsync(GetUserQuery query)
        {
            var user = await _repo.GetByIdAsync(query.Id);
            if (user == null)
                throw new BusinessException(Common.ErrorCode.UserNotFound, $"用户Id={query.Id}不存在");

            return _mapper.Map<UserDto>(user);
        }
    }
}
