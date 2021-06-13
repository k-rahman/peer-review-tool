using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.EventBus.RabbitMQ.MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Submission.Service.API.Domain.Repositories;
using Submission.Service.API.Domain.Services;
using Submission.Service.API.Persistence.Contexts;
using Submission.Service.API.Persistence.Repositories;
using Submission.Service.API.Services;
using Submission.Service.API.Settings;

namespace Submission.Service.API
{
        public class Startup
        {
                readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";
                public IConfiguration Configuration { get; }

                public Startup(IConfiguration configuration)
                {
                        Configuration = configuration;
                }


                // This method gets called by the runtime. Use this method to add services to the container.
                public void ConfigureServices(IServiceCollection services)
                {
                        services.AddScoped<ISubmissionService, SubmissionService>();
                        services.AddScoped<ISubmissionRepository, SubmissionRepository>();
                        services.AddScoped<ISubmissionDeadlinesRepository, SubmissionDeadlinesRepository>();
                        services.AddScoped<ISubmissionDeadlinesService, SubmissionDeadlinesService>();
                        services.AddScoped<IUnitOfWork, UnitOfWork>();

                        services.AddAutoMapper(typeof(Startup));

                        services.AddDbContext<SubmissionContext>(options =>
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
                                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Submission.Service.API", Version = "v1" });
                        });

                        services.AddCors(options =>
                        {
                                options.AddPolicy(name: AllowSpecificOrigins,
                                        builder => builder
                                                // .WithOrigins("http://localhost:3000")
                                                .AllowAnyOrigin()
                                                .AllowAnyMethod()
                                                .AllowAnyHeader());
                        });

                        services.AddAuthentication(options =>
                        {
                                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                        }).AddJwtBearer(options =>
                        {
                                var auth0 = Configuration.GetSection(Auth0Options.Auth0).Get<Auth0Options>();

                                options.Authority = auth0.Authority;
                                options.Audience = auth0.Audience;
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                        NameClaimType = ClaimTypes.NameIdentifier,
                                        RoleClaimType = "https://schemas.peer-review-tool/roles"
                                };
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
                                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Submission.Service.API v1");
                                        c.RoutePrefix = string.Empty;
                                });
                        }

                        app.UseRouting();

                        app.UseCors(AllowSpecificOrigins);

                        app.UseAuthentication();

                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                                endpoints.MapControllers();
                        });
                }
        }
}
