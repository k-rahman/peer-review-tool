using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.EventBus.RabbitMQ.MassTransit;
using Emailing.Service.ApI.Utils;
using Emailing.Service.API.Interfaces;
using Emailing.Service.API.Services;
using Emailing.Service.API.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;

namespace Emailing.Service.API
{
        public class Startup
        {
                public IConfiguration Configuration { get; }
                public Startup(IConfiguration configuration)
                {
                        Configuration = configuration;
                }

                // This method gets called by the runtime. Use this method to add services to the container.
                // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
                public void ConfigureServices(IServiceCollection services)
                {
                        services.AddSingleton<ManagementApiClient>();
                        services.AddSingleton<ManagementApiAccessTokenClient>();

                        services.AddScoped<IMessagingService, MessagingService>();

                        services.AddAutoMapper(typeof(Startup));

                        services.AddMassTransitWithRabbitMq(Configuration);
                }

                // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
                public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
                {
                        if (env.IsDevelopment())
                        {
                                app.UseDeveloperExceptionPage();
                        }

                        app.UseRouting();

                        app.UseEndpoints(endpoints =>
                        {
                                endpoints.MapGet("/", async context =>
                    {
                            await context.Response.WriteAsync("Hello World!");
                    });
                        });
                }
        }
}