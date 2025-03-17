using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartManagement.Data;
using Pomelo.EntityFrameworkCore.MySql;
using System;
using SmartManagement.Core.services;
using SmartManagement.Service.Services;
using SmartManagement.Core.Repositories;
using SmartManagement.Data.Repositories;
using SmartManagement.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 41))));

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
