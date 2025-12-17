using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class BusinessException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public BusinessException(ErrorCode code,string? message = null) :base(message ??code.ToString() )
        {
            ErrorCode = code;
        }
    }
}
