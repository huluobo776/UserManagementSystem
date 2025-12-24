using Application.Interfaces;
using Application.Service;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    /// <summary>
    /// 这里可以添加应用程序服务的扩展方法
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 这里注册应用程序服务，例如：用户服务、订单服务等，以便在依赖注入容器中使用，不需要修改启动项目，不断扩展应用程序功能，不影响其他层，保持良好的分层架构。
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //在这里注册应用程序服务，例如：
            // services.AddTransient<IUserService, UserService>();
            //services.AddSingleton<Interfaces.IUserService,Service.UserService>(); //单例，每次请求都使用同一个实例
            services.AddScoped<Interfaces.IUserService,Service.UserService>(); //
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
