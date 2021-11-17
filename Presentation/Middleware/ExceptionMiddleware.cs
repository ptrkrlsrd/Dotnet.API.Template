using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Template.API.Presentation.Middleware
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(
            IWebHostEnvironment hostingEnvironment,
            ILogger<ExceptionFilter> logger,
            IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogWarning("An unhandled exception occured: {Message}", context.Exception.Message);

            string message = _hostingEnvironment.IsDevelopment() ? context.Exception.Message : "Internal server error";

            context.Result = new JsonResult(message)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
