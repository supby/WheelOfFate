using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WheelOfFate.DataAccess;
using WheelOfFate.Interfaces.DataAccess;
using WheelOfFate.Interfaces.Services;
using WheelOfFate.Models.Entity;
using WheelOfFate.Services;
using WheelOfFate.Web;

namespace WheelOfFate_Web
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
            services.AddMvc();
            
            services.AddDbContext<WheelDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultDB")));

            services.AddScoped<IRepository<Employee>, RepositoryBase<Employee>>();
            services.AddScoped<IRepository<HistoryRecord>, RepositoryBase<HistoryRecord>>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IBAUService, BAUService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            Mappings.Configure();
        }
    }
}
