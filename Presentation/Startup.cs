using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Template.API.Application.Behavior;
using Template.API.Application.DI;
using Template.API.Presentation.Configuration;
using Template.API.Presentation.Middleware;

namespace Template.API
{
    public class Startup
    {
        private readonly ApiVersion _version = new(1, 0);

        public Startup(IWebHostEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            
            Configuration = configurationBuilder.Build();
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var mediatrConfig = new MediatRConfiguration();
            Configuration.GetSection("MediatR").Bind(mediatrConfig);

            var tracingConf = new TracingConfiguration();
            Configuration.GetSection("Tracing").Bind(tracingConf);

            services.Configure<AppsettingsConfiguration>(Configuration);

            services.AddApplicationLayer();
            
            if (tracingConf.Enabled)
                services.AddOpenTelemetry(Program.ApplicationName, new []{ MediatrConstants.ActivitySourceName});
            
            if (mediatrConfig.Middleware.Tracing)
                services.AddTracingMiddleware();
            
            if (mediatrConfig.Middleware.Logging)
                services.AddLoggingMiddleware();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
            });
            
            services.AddSingleton(Configuration);
            
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                var majorVersion = _version.MajorVersion.ToString();
                c.SwaggerDoc(majorVersion, new OpenApiInfo { Title = Program.ApplicationName, Version = majorVersion });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(
                    $"/swagger/{_version.MajorVersion.ToString()}/swagger.json", 
                    $"{Program.ApplicationName} {_version}")
                );
            }
            
            app.UseMiddleware<ErrorHandlerMiddleware>();
            
            app.UseResponseCompression();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
