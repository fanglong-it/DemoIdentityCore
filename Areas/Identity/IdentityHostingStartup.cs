using System;
using DemoIdentityCore.Areas.Identity.Data;
using DemoIdentityCore.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DemoIdentityCore.Areas.Identity.IdentityHostingStartup))]
namespace DemoIdentityCore.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DemoIdentityCoreContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DemoIdentityCoreContextConnection")));

                services.AddDefaultIdentity<DemoIdentityCoreUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()    
                .AddEntityFrameworkStores<DemoIdentityCoreContext>();
            });
        }
    }
}