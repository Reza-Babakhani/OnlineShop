using Application.Constants;
using Infrastructure.Contexts;
using Infrastructure.Identity;
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

            services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();


        }
    }
}
