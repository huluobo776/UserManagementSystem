using Application.Common;
using Application.DTOs.Users;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
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
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAllAsync();
            return Ok(Result<List<UserDto>>.DataResult(users));
        }

        /// <summary>
        /// 通过id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            var u = await _service.GetByIdAsync(id);
            return Ok(u);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDto user)
        {
            var r = await _service.AddAsync(user);
            return Ok(r==0?Result<int>.DataResult(r):Result<int>.Fail("添加用户失败!")); 
            //暂时不知道怎么失败，只能先这样写着，业务失败用异常处理
        }


        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdataUser([FromBody] UpdateUserDto user)
        {
            var r = await _service.UpdateAsync(user);
            return Ok(r?Result.Ok():Result.Fail("更新失败"));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            var r = await _service.DeleteAsync(id);
            return Ok(r ? Result.Ok() : Result.Fail("更新失败"));
        }

    }
}
