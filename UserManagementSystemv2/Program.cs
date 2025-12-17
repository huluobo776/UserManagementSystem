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
using UserManagementSystemv2.Filters;
using Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

// =======================
// MVC + FluentValidation
// =======================

// 1️⃣ MVC（只调用一次）
builder.Services.AddControllers();

// 2️⃣ 启用 FluentValidation 自动校验（必须）
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// 3️⃣ 扫描 Application 层的 Validator
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

// 注册 automapper
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);

// =======================
// Swagger
// =======================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =======================
// Application / Infrastructure
// =======================
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

// =======================
// DbContext
// =======================
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlite(connectionString);
});

// =======================
// Logging
// =======================
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddConfiguration(
    builder.Configuration.GetSection("Logging")
);

var app = builder.Build();

// =======================
// Middleware
// =======================
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();

