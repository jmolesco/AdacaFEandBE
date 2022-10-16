using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Adaca.Api.Infrastructure.ErrorHandling;
using Adaca.Api.Infrastructure.Swagger;
using Adaca.Api.Infrastructure.ApplicationServices;
using Adaca.Api.Infrastructure.AutoMapperProfiles;
using Adaca.Api.Infrastructure.Database;
using Adaca.Api.Infrastructure.Cache;
using Adaca.Api.Infrastructure.Routes;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Adaca.Api.Services;
using Adaca.Api.Controllers;
using Adaca.Api.Controllers.Internal;
using Adaca.Api.Services.Internal;
using Adaca.Api.Repository;
using Adaca.Api.Interfaces;
using System.Net.Http;
using Adaca.Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Adaca.Api
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        public Startup(
          ILogger<Startup> logger,
          IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
            services.ConfigureDatabaseSqlServer(Configuration);
            services.ConfigureCachingService(Configuration);
            services.ConfigureRouteOptions(Configuration);
            services.ConfigureSwagger(Configuration);
            services.ConfigureApplicationServices();
            services.AddTransient(typeof(IBaseApiControllerDependencies<>), typeof(BaseApiControllerDependencies<>));
            services.AddTransient(typeof(IBaseDependenciesService<>), typeof(BasServiceeDependencies<>));
            services.AddSingleton(typeof(ICacheService), typeof(CacheService));
            services.AddClientRepositoryDI(Configuration);
            services.AddTransient<IClientService, ClientService>();
            services.AddDbContext<ClientDBContext>(options => options.UseInMemoryDatabase(databaseName: "ClientDB"));
            services.AddControllers();      
            services.ConfigureErrorHandling();
            services.AddSingleton(Configuration);
            services.ConfigureAutoMapper();        
            services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());
            services.AddHttpClient("ApiHttpsHandler").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                         (httpRequestMessage, cert, cetChain, policyErrors) =>
                         {
                             return true;
                         }
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("LocalDevelopment"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true
            });

            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });            

            var _basePath = Configuration["Swagger:SwaggerBasePath"]; ;
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;                
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    if (httpReq.Headers.ContainsKey("X-Forwarded-Host"))
                    {  
                        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = _basePath } };
                    }
                });

            });
            app.UseSwaggerUI(c => {
                string swaggerPath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerPath}/swagger/v1/swagger.json", "Regis Cessions API Service");              
            });
            app.UseCors(
                options => options.WithOrigins("http://example.com").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
             );
            app.UseRouting();                       
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
