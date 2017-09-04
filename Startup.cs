using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNetCore2Boilerplate.Models;
using BoilerplateData.Work.Interface;
using BoilerplateData.Work;
using BoilerplateData.Repositories.Interfaces;
using BoilerplateData.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using BoilerplateData.DTOs;

namespace AspNetCore2Boilerplate
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
            services.AddAuthentication(o =>
            {
                o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.Cookie.Name = "AuthCookie";
                    options.AccessDeniedPath = "/forbidden";
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.SlidingExpiration = true;
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnValidatePrincipal = LastChangedValidator.ValidateAsync
                    };
                });

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHostingEnvironment hostingEnvironment)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public static class LastChangedValidator
    {
        //rudimentary validation checking; simply checks if the current user is up to date, and updates their credentials if not. 
        public static async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            if (context.Principal.Identity.IsAuthenticated)
            {
                // Pull database from registered DI services.
                var work = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
                var userPrincipal = context.Principal;
                var user = work.UserRepository.Get(int.Parse(userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)));

                // Look for the last changed claim.
                string lastUpdated = userPrincipal.FindFirstValue("LastUpdated");

                if (string.IsNullOrEmpty(lastUpdated) || user.LastUpdated.ToString() != lastUpdated)
                {
                    var principal = ApplicationUser.SetClaims(user);
                    context.ReplacePrincipal(principal);
                    context.ShouldRenew = true;

                    //context.RejectPrincipal();
                    //await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }
        }
    }
}
