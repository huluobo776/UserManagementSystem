using Application;
using Application.Interfaces;
using Domain.Entities;
using UserManagement_System.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Data.Sqlite;
using UserManagement_System.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApplicationServices(); // 注册应用程序服务 ,这里统一注册应用程序服务，例如：用户服务、订单服务等，以便在依赖注入容器中使用
builder.Services.AddInfrastructureServices(); // 注册基础设施服务，这里统一注册基础设施服务，例如：数据库上下文、存储库等，以便在依赖注入容器中使用

// 配置日志记录
builder.Logging.ClearProviders();// 清除默认的日志提供程序
builder.Logging.AddConsole();// 添加控制台日志提供程序
builder.Logging.AddDebug();// 添加调试日志提供程序
//builder.Logging.AddEventLog();// 添加windows事件日志提供程序
builder.Logging.AddEventSourceLogger();// 添加事件源日志提供程序

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));


builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");

    //使用SQLite数据库
    options.UseSqlite(connectionstring);// 使用SQLite数据库，数据库文件名为UserManagementSystem.db
});





var app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>();


// 🔴 必须添加：数据库初始化
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var logger = services.GetRequiredService<ILogger<Program>>();

//    try
//    {
//        var context = services.GetRequiredService<AppDbContext>();

//        // 方法1：使用 EnsureCreated（最简单）
//        var created = context.Database.EnsureCreated();
//        logger.LogInformation($"EnsureCreated 结果: {created}");

//        // 方法2：或者使用 Migrate（如果已经有迁移文件）
//        // context.Database.Migrate();

//        // 验证表是否存在
//        if (context.Database.CanConnect())
//        {
//            // 检查 Users 表
//            try
//            {
//                var usersExist = context.Database.ExecuteSqlRaw(
//                    "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Users'");

//                if (Convert.ToInt32(usersExist) > 0)
//                {
//                    logger.LogInformation("✅ Users 表已存在");
//                }
//                else
//                {
//                    logger.LogWarning("❌ Users 表不存在");
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.LogError(ex, "验证表时出错");
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        logger.LogError(ex, "❌ 数据库初始化失败");
//    }
//}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
