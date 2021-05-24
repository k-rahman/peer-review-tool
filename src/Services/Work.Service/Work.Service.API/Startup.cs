using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.EventBus.RabbitMQ.MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Work.Service.API.Domain.Repositories;
using Work.Service.API.Domain.Services;
using Work.Service.API.Persistence.Contexts;
using Work.Service.API.Persistence.Repositories;
using Work.Service.API.Services;

namespace Work.Service.API
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
                        services.AddScoped<IWorkService, WorkService>();
                        services.AddScoped<IWorkRepository, WorkRepository>();
                        services.AddScoped<IWorksDeadlineRepository, WorksDeadlineRepository>();
                        services.AddScoped<IUnitOfWork, UnitOfWork>();

                        services.AddAutoMapper(typeof(Startup));

                        services.AddDbContext<WorkContext>(options =>
                        {
                                options.UseNpgsql(
                                        Configuration.GetConnectionString("Default"),
                                        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                                );
                        });

                        services.AddMassTransitWithRabbitMq(Configuration);

                        services.AddControllers();

                        services.AddSwaggerGen(c =>
                        {
                                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Work.Service.API", Version = "v1" });
                        });
                }

                // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
                public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
                {
                        if (env.IsDevelopment())
                        {
                                app.UseDeveloperExceptionPage();
                                app.UseSwagger();
                                app.UseSwaggerUI(c =>
                                {
                                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Work.Service.API v1");
                                        c.RoutePrefix = string.Empty;
                                });
                        }

                        app.UseRouting();

                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                                endpoints.MapControllers();
                        });
                }
        }
}
