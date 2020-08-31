using Application.Constants;
using Application.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Infrastructure.IoC
{
  public static class DependencyInjection
    {
        public static void RegisterSevices(this IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(ConnsetionStrings.AppDbContext_Development_ConnectionString));

            services.AddIdentity<ApplicationUser,IdentityRole>(options=>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            services.AddTransient<IDataProtection, DataProtection>();

            services.AddScoped<IEmailSender, EmailSender>();


        }
    }
}
