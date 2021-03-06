﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.App;
using DAL;
using DAL.Core;
using DAL.EF;
using DAL.JSON;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp
{
    public class Startup
    {
        private static readonly string InMemoryDbName = Guid.NewGuid().ToString();

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

            
            
            
            #region EF based dal

            //services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(InMemoryDbName));
            //services.AddScoped<IDataContext, AppDbContext>();
            //services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();

            #endregion

            #region JSON based dal

            services.Configure<AppJSONContextOptions>(options =>
            {
                options.DataPath = "/Users/Akaver/Magister/TarkvaraArhitektuur/CleanArchitectureDemo/Data/";
            });
            services.AddScoped<IDataContext, AppJSONContext>();
            services.AddScoped<IAppUnitOfWork, AppJSONUnitOfWork>();

            #endregion

            
            
            services.AddScoped<IAppBLL, AppBLL>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // seed the data, if EF database is available
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<AppDbContext>()?.Database.EnsureCreated();
            }
        }
    }
}