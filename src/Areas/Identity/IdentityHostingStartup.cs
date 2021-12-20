using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using src.Areas.Identity.Data;

[assembly: HostingStartup(typeof(src.Areas.Identity.IdentityHostingStartup))]
namespace src.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            /*
            Dit heb ik geoutcomment doordat dit ook wordt gedaan in de normale startup.cs

            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MijnContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MijnContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<MijnContext>();
            });
            */
        }
    }
}