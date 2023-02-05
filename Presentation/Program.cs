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

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment env = builder.Environment;
IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();
        
IConfigurationRoot configurationRoot = configurationBuilder.Build();
var configuration = configurationRoot.Get<AppsettingsConfiguration>();
builder.Services.Configure<AppsettingsConfiguration>(configurationRoot);

builder.Services.AddControllers();
builder.Services.AddSingleton(configuration);
builder.Services.AddApplicationLayer();

bool tracingEnabled = configuration.Tracing.Enabled;
if (tracingEnabled)
    builder.Services.AddOpenTelemetry(configuration.ApplicationName, new []{ MediatrConstants.ActivitySourceName});

if (tracingEnabled && configuration.Mediatr.Middleware.Tracing)
    builder.Services.AddTracingMiddleware();

if (configuration.Mediatr.Middleware.Logging)
    builder.Services.AddLoggingMiddleware();

builder.Services.AddResponseCompression(options => {
    if (configuration.Server.Compression.UseBrotli)
        options.Providers.Add<BrotliCompressionProvider>();
    if (configuration.Server.Compression.UseGZip)
        options.Providers.Add<GzipCompressionProvider>();
});

ApiVersion version = new(1, 0);
builder.Services.AddApiVersioning(options => {
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = version;
});
        
builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddSwaggerGen(options => {
    var majorVersion = version.MajorVersion.ToString();
    options.SwaggerDoc(majorVersion, new OpenApiInfo { Title = configuration.ApplicationName, Version = majorVersion });
});

WebApplication app = builder.Build();

app.UseWhen(_ => configuration.Server.UseHTTPS, a => {
    a.UseHttpsRedirection();
});

app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{configuration.ApplicationName} {version}"));
app.MapControllers();

app.Run();
