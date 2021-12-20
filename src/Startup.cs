using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace src
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            //Deze moet later verwijderd worden,Doordat we gebruik maken van een andere DB provider 
            services.AddDbContext<MijnContext>(o=>
                        o.UseSqlite("Data source= Database.db"));

            //dit is nodig voor de identity. Echter is deze nog niet van toepassing
            services.AddIdentity<srcUser, IdentityRole>()
                                .AddEntityFrameworkStores<MijnContext>()
                                .AddDefaultUI()
                                .AddDefaultTokenProviders();
                                

            //Dit is nodig voor het gebruik van signal R
            services.AddSignalR();

            /* 
                Dit moet worden gedaan doordat het framework een preventie
                heeft voor een Cross site attack.

                Hiermee stel je vast dat je van deze website wel berichten
                zou willen ontvangen
            */
            services.AddCors(options=>
                options.AddDefaultPolicy(builder =>
                {
                    //dit moet later ook terug veranderd worden naar de website die gebruikt gaat worden
                    builder.WithOrigins("https://localhost:5001")
                            .AllowCredentials();
                })
            );
                services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Dit is nodig voor de werking van signalR
            app.UseCors(); 

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Deze maphub moet hier geinitialiseerd zijn
                //Deze zorgt voor de verbinding
                endpoints.MapHub<ChatHub>("/chatHub");
                //endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
