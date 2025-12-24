using Application.Handlers;
using Domain.Interfaces;
using Application.Exceptions;
using Application.Common;

namespace Application.Comands.UpdateUser
{
    public class UpdateUserHandler : IUpdateUserHandler
    {
        private readonly IUserRepository _repo;
        public UpdateUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> HandleAsync(UpdateUserCommand command)
        {
            var dto = command.Dto;

            var existing = await _repo.GetByIdAsync(dto.Id);
            if (existing == null)
                throw new BusinessException(ErrorCode.UserNotFound, $"用户{dto.Id}不存在");

            //如果更新邮箱，需要检查唯一性
            if (!string.Equals(existing.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var byEmail = await _repo.GetByEmailAsync(dto.Email);
                if (byEmail != null && byEmail.Id != dto.Id)
                    throw new BusinessException(ErrorCode.UserAlreadyExists, "邮箱已被占用");
            }

            //执行字段更新 -- 推荐只更新必要字段 ，避免replace全量覆盖
            existing.Name = dto.Name;
            existing.Email = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.Password))
                existing.Password = dto.Password;

            return await _repo.UpdataAsync(existing);//仓储层只做更新动作

        }
    }
}
