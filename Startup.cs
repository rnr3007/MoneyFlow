using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MoneyFlow.Data;
using MoneyFlow.Services;
using System.Collections.Generic;
using MoneyFlow.Services.Middleware;
using MoneyFlow.Utils;

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
            
            List<string> protectedBranch = new List<string> 
            {
                "/user", "/file", "/expense", "/expenses", "/dashboard", "/incomes", "/motivations"
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
