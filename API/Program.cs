using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Template.API;
using Template.API.Application.Behavior;
using Template.API.Application.DI;
using Template.API.Presentation.Configuration;
using Template.API.Presentation.Middleware;


WebApplicationBuilder SetupBuilder(WebApplicationBuilder newBuilder, ApiVersion apiVersion, out AppsettingsConfiguration appsettingsConfiguration)
{
    IWebHostEnvironment environment = newBuilder.Environment;
    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();

    IConfigurationRoot configurationRoot = configurationBuilder.Build();
    appsettingsConfiguration = configurationRoot.Get<AppsettingsConfiguration>();
    AppsettingsConfiguration conf = appsettingsConfiguration;
    
    newBuilder.Services.Configure<AppsettingsConfiguration>(configurationRoot);
    newBuilder.Services.AddControllers();
    newBuilder.Services.AddSingleton(appsettingsConfiguration);
    newBuilder.Services.AddApplicationLayer();

    bool tracingEnabled = conf.Tracing.Enabled;
    if (tracingEnabled)
        newBuilder.Services.AddOpenTelemetry(conf.ApplicationName, new[] {
            MediatrConstants.ActivitySourceName
        });

    if (tracingEnabled && conf.Mediatr.Middleware.Tracing)
        newBuilder.Services.AddTracingMiddleware();

    if (conf.Mediatr.Middleware.Logging)
        newBuilder.Services.AddLoggingMiddleware();

    newBuilder.Services.AddResponseCompression(options =>
    {
        if (conf.Server.Compression.UseBrotli)
            options.Providers.Add<BrotliCompressionProvider>();
        if (conf.Server.Compression.UseGZip)
            options.Providers.Add<GzipCompressionProvider>();
    });

    newBuilder.Services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = apiVersion;
    });

    newBuilder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)));
    newBuilder.Services.AddSwaggerGen(options =>
    {
        var majorVersion = apiVersion.MajorVersion.ToString();
        options.SwaggerDoc(majorVersion, new OpenApiInfo {
            Title = conf.ApplicationName,
            Version = majorVersion
        });
    });
    return newBuilder;
}

ApiVersion version = new(1, 0);
WebApplicationBuilder builder = SetupBuilder(WebApplication.CreateBuilder(args), version, out AppsettingsConfiguration configuration);

WebApplication NewApplication(WebApplicationBuilder webApplicationBuilder, AppsettingsConfiguration appsettingsConfiguration, ApiVersion apiVersion)
{
    WebApplication app = webApplicationBuilder.Build();
    app.UseWhen(_ => appsettingsConfiguration.Server.UseHTTPS, a => { a.UseHttpsRedirection(); });
    app.UseAuthorization();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{apiVersion.ToString()}/swagger.json", $"{appsettingsConfiguration.ApplicationName} {apiVersion}"));
    app.MapControllers();
    return app;
}

WebApplication app = NewApplication(builder, configuration, version);
app.Run();
