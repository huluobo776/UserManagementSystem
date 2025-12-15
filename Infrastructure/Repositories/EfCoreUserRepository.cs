using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore; //持久化层的数据库上下文类


namespace Infrastructure.Repositories
{
    public class EfCoreUserRepository : Domain.Interfaces.IUserRepository
    {
        private readonly AppDbContext _context;
        public EfCoreUserRepository(AppDbContext context)
        {
            _context = context;
        }

        //在这里实现基于EF Core的用户数据访问方法


        /// <summary>
        /// 通过id查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User?> GetByIdAsync(int id) => await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);//添加用户实体到上下文
            await _context.SaveChangesAsync();//保存更改到数据库
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(User user)
        {
            var u = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (u == null) return false;

            _context.Users.Update(user);//更新用户实体
            await _context.SaveChangesAsync();//保存更改到数据库                         
            return true;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var a = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (a == null) return false;
            _context.Users.Remove(a);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllAsync()
        {
            var userLsit = await _context.Users.AsNoTracking().ToListAsync();
            return userLsit;
        }
    }
}
