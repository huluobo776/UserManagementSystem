using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            //在这里注册基础设施服务，例如：
            // services.AddTransient<IUserRepository, InmeMoryUserRepository>();
            //services.AddSingleton<Domain.Interfaces.IUserRepository, Repositories.InmeMoryUserRepository>();//接口与实现类的映射   //单例 ，每次请求都使用同一个实例
            services.AddScoped<Domain.Interfaces.IUserRepository, Repositories.EfCoreUserRepository>();//接口与实现类的映射   //作用于每个请求的生命周期，每个请求使用同一个实例，不同请求使用不同实例
            return services;
        }
    }
}
