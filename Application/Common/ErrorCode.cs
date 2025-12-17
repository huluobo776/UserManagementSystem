using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    /// <summary>
    /// 错误码
    /// </summary>
    public enum ErrorCode
    {
        Unknown = 0,
        
        //User模块
        UserNotFound = 10001,

        UserAlreadyExists = 10002,

        //Auth模块
        Unauthorized = 20001,
        Forbidden = 20002,

        ValidationError = 90001,

    }
}
