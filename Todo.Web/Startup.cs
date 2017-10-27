using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Todo.Bll.Fcd;
using Todo.Bll.Interfaces.Facades;
using Todo.Bll.Interfaces.Identity;
using Todo.Bll.Services;
using Todo.Common.Models;
using Todo.Dal;
using Todo.Dal.Interfaces;
using Todo.Dal.Repositories;
using Todo.Web.Middleware;

namespace Todo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            ConfigureIdentity(services);
            RegisterMyServices(services);
            services.AddAutoMapper();
            services.AddMvc();
            ConfigureSwagger(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            // For testing staging mode in error handling
//            env.EnvironmentName = EnvironmentName.Staging;

            // Log all request comming to app
            app.UseMiddleware<LoggingMiddleware>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                // Exception handling on production
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/Error");
            }
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
            });

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private static void RegisterMyServices(IServiceCollection services)
        {
            // Facades
            services.AddScoped<IBaseFacade, BaseFacade>();
            services.AddScoped<ITodoFacade, TodoFacade>();
            services.AddScoped<ICardFacade, CardFacade>();
            services.AddTransient<IAccountFacade, AccountFacade>();
            // Repositories
            services.AddScoped(typeof(ITodoRepository<TodoItem>), typeof(TodoRepository));
            services.AddTransient(typeof(IAccountRepository<User>), typeof(AccountRepository));
            services.AddTransient(typeof(ICardRepository<Card>), typeof(CardRepository));
            // Rest
            services.AddTransient<IMessageService, FileMessageService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<TodoContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.SignIn.RequireConfirmedEmail = true;
            });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Info
                {
                    Title = "Todo API",
                    Version = "v1",
                    Description = "A simple example ASP.NET Core Web API",
                    Contact = new Contact { Name = "Marcin Urban", Email = "marcinn.urban@gmail.com" }
                });
                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Todo.Web.xml");
                opt.IncludeXmlComments(xmlPath);
            });
        }
    }
}
