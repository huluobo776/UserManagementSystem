using Application.Comands.UpdateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public  interface IUpdateUserHandler
    {
        Task<bool> HandleAsync(UpdateUserCommand command); 
    }
}
