using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MoneyFlow.Data;
using MoneyFlow.Services;
using MoneyFlow.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConn"))
            );
            
            services.AddHttpContextAccessor();

            services.AddScoped<UserService>();

            services.AddScoped<ExpenseService>();

            services.AddScoped<FileService>();

            services.AddScoped<IncomeService>();

            services.AddScoped<MotivationService>();

            services.AddScoped<SavingsService>();

            services.AddTransient<DatabaseContext>();

            services.AddControllersWithViews();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.Cookie.Name = "TokenBearer";
                option.ExpireTimeSpan = TimeSpan.FromDays(1);
                option.LoginPath = "/user/login";
                option.LogoutPath = "/user/logout";
                option.SlidingExpiration = true;
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            
            FileUtilites.Configure(env);
            
            app.UseAuthentication();
            
            app.UseAuthorization();

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
