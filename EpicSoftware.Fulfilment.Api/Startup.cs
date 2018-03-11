using System;
using EpicSoftware.Fulfilment.Api.MappingProfiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using EpicSoftware.Fulfilment.Context;
using EpicSoftware.Fulfilment.Repository.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EpicSoftware.Fulfilment.Api
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
            //TODO Add Auth
            services.AddCors(
                options => options.AddPolicy("AllowCors",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    })
            );

            var connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION");

            //Determine the require DB from the app configuration
            switch (Configuration.GetValue<string>("DbProvider"))
            {
                case "MySQL": 
                    CreateMySqlContext(services, connectionString);
                    break;
                case "SqlServer":
                    CreateSqlServerContext(services, connectionString);
                    break;
                case "Postgres":
                    CreatePostgresConnection(services, connectionString);
                    break;
                default:
                    CreateSqlServerContext(services, connectionString);
                    break;
            }
            
            //Repositories
            services.AddTransient<IOrderRepository, OrderRepository>();
            
            //Services
            services.AddTransient<OrdersService.OrdersService, OrdersService.OrdersService>();
            
            //Automapper
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Fulfilment API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });
            
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fulfilment API");
            });

            app.UseMvc();
        }

        /// <summary>
        /// Create a MySql Context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="conn"></param>
        private static void CreateMySqlContext(IServiceCollection services, string conn)
        {
            services.AddDbContext<FulfilmentContext>(options =>
            {
                options.UseMySql(conn);
            });
        }

        /// <summary>
        /// Create a SQL Server Context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="conn"></param>
        private static void CreateSqlServerContext(IServiceCollection services, string conn)
        {
            services.AddDbContext<FulfilmentContext>(options =>
            {
                options.UseSqlServer(conn);
            });
        }

        /// <summary>
        /// Create a Postgres Context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="conn"></param>
        private static void CreatePostgresConnection(IServiceCollection services, string conn)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<FulfilmentContext>(options =>
            {
                options.UseNpgsql(conn);
            });
        }
    }
}