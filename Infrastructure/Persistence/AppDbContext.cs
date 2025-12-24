using Domain.Entities;
using Microsoft.EntityFrameworkCore;

//持久化层的数据库上下文类
namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>(); //用户实体集合
        public DbSet<Product> Products => Set<Product>(); 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //配置User实体
            modelBuilder.Entity<User>(entity =>
            {
                //1.表名和架构配置
                entity.ToTable("Users"); //指定表名为Users
                //entity.HasKey(e => e.Id); //主键
                //2.主键配置
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); //自增主键
                //3.属性配置
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasComment("用户姓名"); //名称属性
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200).HasComment("邮箱地址"); //电子邮件属性
                entity.Property(e => e.Password).IsRequired().HasMaxLength(100).HasComment("密码（加密存储）"); //密码属性


                // 7. 值转换（将 C# 类型转换为数据库类型）
                //entity.Property(e => e.Status)
                //    .HasConversion(
                //        v => v.ToString(),        // 存储到数据库
                //        v => (UserStatus)Enum.Parse(typeof(UserStatus), v)  // 从数据库读取
                //    );


                // 创建唯一索引
                entity.HasIndex(e => e.Email) //设置Email为唯一索引
                    .IsUnique();
            });


            //配置Product实体
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();//自增Id
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasComment("产品名称");
                entity.Property(e => e.Sku).IsRequired().HasMaxLength(50).HasComment("库存单位");
                entity.Property(e => e.Price).IsRequired().HasMaxLength(10).HasComment("价格");
                entity.Property(e => e.Stock).IsRequired().HasMaxLength(20).HasComment("库存数量");

                entity.HasIndex(e => e.Sku) //设置Email为唯一索引
                .IsUnique();
            });



        }

    }
}
