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
            builder.ConfigureServices((context, services) => {
<<<<<<< HEAD
=======
                /*
                Deze laat ik er in. Dit is voor de locale database als SQL server
                services.AddDbContext<srcContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("srcContextConnection")));

                services.AddDefaultIdentity<srcUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<srcContext>();
            });
        }
    }
}
*/
>>>>>>> 8389422d02a7ce225d0e88b71766fa905e22621d
                services.AddDbContext<MijnContext>(options =>
                
                    //options.UseSqlServer(
                     //   context.Configuration.GetConnectionString("poging4IdentityDbContextConnection")));
            
                options.UseSqlite("Data Source=database.db"));
                /*
                    services.AddDefaultIdentity<poging4IdentityDbContext>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<poging4IdentityDbContext>();
                    */
                    //Dit is voor de identity Roles waar je ook eigen kan maken vanuit de slides
                }
            );
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 8389422d02a7ce225d0e88b71766fa905e22621d
