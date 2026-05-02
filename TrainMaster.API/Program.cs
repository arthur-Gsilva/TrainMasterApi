using TrainMaster.Infrastructure;
using TrainMaster.API.Middleware;
using TrainMaster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using TrainMaster.Application.Interfaces;
using TrainMaster.Application.Services;
using TrainMaster.Domain.Interfaces;
using TrainMaster.Infrastructure.Services;

using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity.Data;


var builder = WebApplication.CreateBuilder(args);

// ── Services ────────────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssemblyContaining<TrainMaster.Application.DTOs.RegisterRequest>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
