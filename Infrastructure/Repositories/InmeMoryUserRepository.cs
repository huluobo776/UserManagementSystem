using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Infrastructure.Repositories
{
    public class InmeMoryUserRepository //: IUserRepository  这里的接口先注释掉，避免报错 后续需要修改方法
    {
        private readonly List<User> _store = new List<User>();


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        public Task AddAsync(User user)
        {
            _store.Add(user);
            return Task.CompletedTask;
        }


        public Task<bool> DeleteAsync(int id)
        {
            var user = _store.FirstOrDefault(x => x.Id == id);
            if (user == null) return Task.FromResult(false);
            _store.Remove(user);

            return Task.FromResult(true);
        }


        public Task<List<User>> GetAllAsync()
        {
            return Task.FromResult(_store);
        }


        public async Task<User?> GetByIdAsync(int id) => await Task.FromResult(_store.Find(user => user.Id == id));

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> UpdateAsync(User user)
        {
            var existingUser = _store.FirstOrDefault(x => x.Id == user.Id);
            if (existingUser == null) return Task.FromResult(false);

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            return Task.FromResult(true);
        }

    }
}
