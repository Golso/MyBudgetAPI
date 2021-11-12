using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyBudgetApi.Data;
using MyBudgetApi.Data.Abstractions;
using MyBudgetApi.Data.Context;
using MyBudgetApi.Data.Dtos;
using MyBudgetApi.Data.Models;
using MyBudgetApi.Data.Repositories;
using MyBudgetApi.Middleware;
using MyBudgetApi.Services;
using MyBudgetApi.Services.Abstractions;
using System.Text;

namespace MyBudgetApi
{
    public static class ServiceExtensions
    {
        public static void ConfigureAuthenticationAndJwtBearer(this IServiceCollection services, AuthenticationSettings authenticationSettings)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });
        }

        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
        }

        public static void AddMiddleware(this IServiceCollection services)
        {
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<RequestTimeMiddleware>();
        }

        public static void AddDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BudgetDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DbConection")));

        }
    }
}
