using System;
using System.IO;
using System.Reflection;
using Andead.SmartHome.Constants;
using Andead.SmartHome.Mqtt;
using Andead.SmartHome.Presentation.API.Extensions;
using Andead.SmartHome.Presentation.API.Filters;
using Andead.SmartHome.Presentation.Hubs;
using Andead.SmartHome.Services;
using Andead.SmartHome.Services.Interfaces;
using Andead.SmartHome.UnitOfWork;
using Andead.SmartHome.UnitOfWork.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Andead.SmartHome.Presentation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddSignalR();

            var connectionString = Configuration[Settings.CONNECTION_STRING_VARIABLE];
            services.AddSingleton<IRepositoryFactory>(new RepositoryFactory(connectionString));

            services.AddSingleton(new SystemCancellationToken());

            services.AddMemoryCache();
            services.AddMiniProfiler(options => options.RouteBasePath = "/profiler").AddEntityFramework();

            services.AddAutoMapper(typeof(Startup));
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Smart Home REST API"
                    });

                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });

            foreach (var singletonService in Reflection.GetClassesImplementingInterface<IService>())
            {
                services.AddSingleton(singletonService);
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMiniProfiler();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<LogHub>("/log");
                endpoints.MapHub<DeviceStateHub>("/states");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "src";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            StartServices(serviceProvider);
        }

        private void StartServices(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<MqttService>().Start();
            serviceProvider.GetRequiredService<WorkflowService>().Start();
        }
    }
}
