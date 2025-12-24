using Application.Comands.CreateProductCommand;
using Application.Comands.CreateUserCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public interface ICreateProductHandler
    {
        Task<int> HandleAsync(CreateProductCommand command);
    }
}
