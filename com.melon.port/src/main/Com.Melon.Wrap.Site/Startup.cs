using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Com.Melon.Wrap.Site.Models;
using Com.Melon.IdentityAccess.Port.Adapter.Persistance;
using Com.Melon.IdentityAccess.Application;
using MediatR;
using Com.Melon.IdentityAccess.Domain;
using Com.Melon.Wrap.Site.Core.Application;
using Com.Melon.Wrap.Site.Core.Domain;
using Com.Melon.Wrap.Site.Core.Port.Adapter.Persistence;
using Com.Melon.Wrap.Site.Core.Port.Adapter.Mvc;
using Microsoft.Extensions.Options;

namespace Com.Melon.Wrap.Site
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

            services.AddMvc(opts=>
            {
                // opts.Filters.Add(new MelonAuthorizationFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // user
            services.AddDbContext<IdentityAccessDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("IdentityConnectionString")));

            services.AddMediatR(typeof(RegisterUserCommandHandler).Assembly);
          
            services.AddTransient<IRegisterUserService, RegisterUserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            // session 
            services.AddDbContext<WrapSiteCoreDbContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("WebConnectionString")));
            services.AddMediatR(typeof(CreateSessionCommandHandler).Assembly);
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<ISessionRepository, SessionRepository>();
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            // app.AddCookies("Cookies");
            app.UseMiddleware<CookieAuthenticationMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "MyArea",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
