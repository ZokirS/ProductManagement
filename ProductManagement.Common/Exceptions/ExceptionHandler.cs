using ProductManagement.Common.Exceptions.CustomExceptions;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProductManagement.Common.Helpers;
using Newtonsoft.Json;

namespace ProductManagement.Common.Exceptions
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;
        private const string DefaultErrorMessage = "Something went wrong. Please, contact administrator";

        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BaseException e)
            {
                var stackTrace = e.StackTrace?.Split("\n").FirstOrDefault();
                _logger.LogError("Request Failure at {@RequestName} Message: {@Error} at {@DateTimeUtc}", context.Request.Path, e.Message, DateTime.UtcNow);
                _logger.LogError("Message: {@Error} at {@DateTimeUtc}", e.Message, DateTime.UtcNow);
                _logger.LogError("Stack Trace:{@StackTrace}:", stackTrace);
                await HandleError(context, e);
            }
            catch (Exception e)
            {
                var stackTrace = e.StackTrace?.Split("\n").FirstOrDefault();
                _logger.LogError("Request Failure at {@RequestName} Message: {@Error} at {@DateTimeUtc}", context.Request.Path, e.Message, DateTime.UtcNow);
                _logger.LogError("Message: {@Error} at {@DateTimeUtc}", e.Message, DateTime.UtcNow);
                _logger.LogError("Stack Trace:{@StackTrace}", stackTrace);
                await HandleError(context, e);
            }
        }

        private static Task HandleError(HttpContext context, Exception exception)
        {
            var error = new ErrorModel();

            if (exception is BaseException baseException)
            {
                error.ErrorCode = baseException.ErrorCode;
                error.ErrorMessage = baseException.Message;
            }
            else
            {
                error.ErrorCode = -10;
                error.ErrorMessage = DefaultErrorMessage;
            }

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}