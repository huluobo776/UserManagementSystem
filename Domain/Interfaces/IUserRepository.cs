using Domain.Entities; //引用实体

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        //错误例子：Public Task<User?> GetUserByIdAsync(int id);
        //其一，接口成员默认是public和abstract的，可以省略这些修饰符，
        //其二接口是一个领域模型的契约，
        //它是一个“领域对象集合”的抽象，不需要具体的名称例如 GetUserById
        Task<User> GetByIdAsync(int id);
        Task<int> AddAsync(User user);

        Task<bool> UpdataAsync(User user);
        Task<bool> DeleteAsync(int id);

        Task<List<User>> GetAllAsync();

        Task<User?> GetByEmailAsync(string email);
    }
}
