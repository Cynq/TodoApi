using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Dal;
using Microsoft.EntityFrameworkCore;
using Todo.Bll.Fcd;
using Todo.Common.Interfaces.Facades;
using Todo.Common.Interfaces.Repositories;
using Todo.Dal.Repositories;

namespace Todo.Web
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
            services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            RegisterMyServices(services);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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
            });
        }
        private static void RegisterMyServices(IServiceCollection services)
        {
            services.AddScoped<IBaseFacade, BaseFacade>();
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<ITodoFacade, TodoFacade>();
            services.AddScoped<ITodoRepository, TodoRepository>();
        }
    }
}
