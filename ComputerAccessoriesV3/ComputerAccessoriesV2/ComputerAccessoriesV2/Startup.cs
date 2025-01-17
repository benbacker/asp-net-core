using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using ComputerAccessoriesV2.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using ComputerAccessoriesV2.Models;
using ComputerAccessoriesV2.Ultilities;
using ComputerAccessoriesV2.DI;
using ComputerAccessoriesV2.SchedulerTask;
using Coravel;

namespace ComputerAccessoriesV2
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
            services.AddScheduler();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("IdentityConnection")));

            services.AddDbContext<ComputerAccessoriesV2Context>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<QueryDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("QueryConnection")));
            services.AddSession(options =>
            {
                options.IdleTimeout = new TimeSpan(0, 15, 0);
                options.Cookie.IsEssential = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services.AddTransient<UpdateProductVisitorCount>();

            services.AddIdentity<MyUsers, IdentityRole<int>>(options =>
            {
                //options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            services.AddSession();

            services.AddAntiforgery(o => o.HeaderName = "CSRF-TOKEN");

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSingleton<IRedis>(new RedisImpl());
            ;
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.AdminAccess, policy =>
                {
                    policy.RequireRole(SD.Admin, SD.SupperAdmin);
                });

                options.AddPolicy(Policy.AdminModify, policy =>
                {
                    policy.RequireRole(SD.SupperAdmin);
                });

                options.AddPolicy(Policy.ProfileModify, policy =>
                {
                    policy.RequireRole(SD.Customer, SD.Admin, SD.SupperAdmin, SD.Shipper);
                });

                options.AddPolicy(Policy.Customer, policy =>
                {
                    policy.RequireRole(SD.Customer);
                });
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Customer/Account/SignIn";
                options.AccessDeniedPath = "/Customer/Account/AccessDeny";
                options.Cookie.IsEssential = true;
            });
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var provider = app.ApplicationServices;

            provider.UseScheduler(scheduler =>
            {
                scheduler.Schedule<UpdateProductVisitorCount>()
                        .EveryThirtyMinutes();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                //endpoints.MapControllerRoute(
                //    name: "areas",
                //    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
                //    );

                endpoints.MapControllerRoute(
                    name: "default_route",
                    pattern: "{area}/{controller}/{action}/{id?}",
                    defaults: new { area = "Customer", controller = "Home", action = "Index" }
                    );

                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
