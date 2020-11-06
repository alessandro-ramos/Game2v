using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game2v.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Game2v
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Enable TempData
            services.Configure<CookieTempDataProviderOptions>(options => {
                options.Cookie.IsEssential = true;
            });

            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // BEGIN Authentication engine
            services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<AppIdentityDbContext>(options =>
            //    options.UseSqlServer(this.config.GetConnectionString("AppDb")));
            services.AddIdentity<AppIdentityUser, AppIdentityRole>(
                x =>
                    {
                        x.Password.RequiredLength = 6;
                        x.Password.RequireUppercase = false;
                        x.Password.RequireLowercase = true;
                        x.Password.RequireDigit = true;
                        x.Password.RequireNonAlphanumeric = false;
                    }         
            ).AddEntityFrameworkStores<AppIdentityDbContext>();
            
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Security/SignIn";
                opt.AccessDeniedPath = "/Security/AccessDenied";
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => o.LoginPath = new PathString("/Security/SignIn"));
            // END Authentication
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            // app.UseCookieAuthentication(); // Obsolete 
            
            app.UseMvc();
        }
    }
}
