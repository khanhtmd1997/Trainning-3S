using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskTranning.Filters;
using TaskTranning.Models;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.Validator;
using IProductServices = TaskTranning.Services.IProductServices;
using IStockServices = TaskTranning.Services.IStockServices;

namespace TaskTranning
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
//            services.Configure<CookiePolicyOptions>(options =>
//            {
//                options.CheckConsentNeeded = context => true;
//                options.MinimumSameSitePolicy = SameSiteMode.None;
//            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            
            services.AddScoped<FiltersSample>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IStoreServices, StoreServices>();
            services.AddTransient<ICategoryServices, CategoryServices>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<IBrandServices, BrandServices>();
            services.AddTransient<IStockServices, StockServices>();
            services.AddTransient<CodeFirstDataContext>();
            services.AddAutoMapper();            
            
            services.AddDbContext<CodeFirstDataContext>(item => item.UseSqlServer(Configuration.GetConnectionString("myconn")));
            services.AddDbContext<CodeFirstDataContext>();

            services.AddSingleton<ResourcesServices<UserResource>>();
            services.AddSingleton<ResourcesServices<StoreResource>>();
            services.AddSingleton<ResourcesServices<StockResource>>();
            services.AddSingleton<ResourcesServices<ProductResource>>();
            services.AddSingleton<ResourcesServices<CategoryResource>>();
            services.AddSingleton<ResourcesServices<BrandResource>>();
            services.AddSingleton<ResourcesServices<LoginResource>>();
            services.AddSingleton<ResourcesServices<CommonResource>>();
            services.AddSingleton<ResourcesServices<LayoutManagementResource>>();
            services.AddSingleton<ResourcesServices<HomeResource>>();
            services.AddSingleton<ResourcesServices<ErrorResource>>();

            #region snippet1

            #endregion
            services.AddLocalization(options => options.ResourcesPath = "Resources"); 
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("vi-VN")                
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = new PathString("/Error/401"); 
                    options.LoginPath = new PathString("/Login/LoginForm");
                    options.ReturnUrlParameter = "RequestPath";
                    options.ExpireTimeSpan = TimeSpan.FromHours(24);
                    options.SlidingExpiration = true;
                });
            
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddDataAnnotationsLocalization()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddUserValidator>())
                .AddViewLocalization(); 
            
            services.AddHttpContextAccessor();
            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseHttpsRedirection();
            app.UseRequestLocalization();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.Use(async (context, next) =>
            {
                await next();
                if (!context.Response.HasStarted && context.Response.StatusCode != StatusCodes.Status200OK)
                {
                    if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                    {
                        context.Request.Path = "/Error/404";
                        await next();
                    }
                        
                    else if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                    {
                        context.Request.Path = "/Error/401";
                        await next();
                    }
                    else if (context.Response.StatusCode == StatusCodes.Status400BadRequest)
                    {
                        context.Request.Path = "/Error/400";
                        await next();
                    }
                } 
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
            });
            
        }
    }
}