using Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comands.CreateUserCommand
{
    // 创建用户命令
    public record CreateUserCommand(CreateUserDto Dto);
}
