using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using UserManagement_System.Models;

namespace UserManagement_System.Controllers
{
    [ApiController]
    [Route("api/Users/[action]")] //真实项目最好每一个都指定路由，避免冲突，同时修改方法名也不会影响路由
    public class UserController : ControllerBase
    {
        //控制器的职责是处理用户请求，调用应用服务，并返回响应
        public readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Result<List<User>>>> GetAllUsers()
        {
            var users = await _service.GetAllAsync();
            return users;
        }

        /// <summary>
        /// 通过id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Result<User?>>> GetUserById([FromQuery] int id)
        {
            var u = await _service.GetByIdAsync(id);
            return u;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Result>> AddUser([FromBody] userInput user)
        {
            var r = await _service.AddAsync(user);
            return r;
        }


        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Result>> UpdataUser([FromBody] userInput user)
        {
            var r = await _service.UpdateAsync(user);
            return r;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<Result>> DeleteUser([FromQuery] int id)
        {
            var r = await _service.DeleteAsync(id);
            return r;
        }

    }
}
