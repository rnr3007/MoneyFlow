using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using MoneyFlow.Context;
using MoneyFlow.Services;
using System.Collections.Generic;
using MoneyFlow.Middleware;
using System;

namespace MoneyFlow
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfiguration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static IConfiguration StaticConfiguration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConn"))
            );
            
            services.AddHttpContextAccessor();

            services.AddScoped<UserService>();
            services.AddScoped<ProductService>();

            services.AddTransient<UserContext>();

            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // app.UseHttpsRedirection();

            List<string> protectedBranch = new List<string> 
            {
                "/product", "/user"
            };
            
            List<string> unprotectedBranch = new List<string> 
            {
                "/user/register"
            };

            app.UseWhen(context => 
            (
                !unprotectedBranch.Contains((string)context.Request.Path)
                && protectedBranch.Exists(c => ((string)context.Request.Path).StartsWith(c))
            ), builder =>
            {
                builder.UseMiddleware<AuthValidation>();
                builder.UseAuthorization();
            });

            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseStaticFiles();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
