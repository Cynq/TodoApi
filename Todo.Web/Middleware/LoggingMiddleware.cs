using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Todo.Common.Models;
using Todo.Dal;

namespace Todo.Web.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext, TodoContext dbContext)
        {
            var logModel = new RequestResponseLog();

            var orginalBody = httpContext.Response.Body;

            try
            {
                await LogRequest(httpContext, logModel);

                using (var memoryStream = new MemoryStream())
                {
                    httpContext.Response.Body = memoryStream;
                    await _next(httpContext);
                    await LogResponse(httpContext, logModel, memoryStream, orginalBody);
                }

                await SaveLogModel(dbContext, logModel);
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, ex.Message);
                //allows exception handling middleware to deal with things
                throw;
            }
            finally
            {
                httpContext.Response.Body = orginalBody;
            }
        }

        private async Task SaveLogModel(TodoContext dbContext, RequestResponseLog log)
        {
            await dbContext.Logs.AddAsync(log);
            await dbContext.SaveChangesAsync();
        }

        private async Task LogResponse(HttpContext httpContext, RequestResponseLog log, MemoryStream memoryStream, Stream orginalBody)
        {
            log.ResponseBody = await GetResponseBody(memoryStream, orginalBody);
            log.ResponseStatusCode = httpContext.Response.StatusCode;
            foreach (var header in httpContext.Response.Headers)
                log.RequestHeaders += header;

            // Log response
            _logger.LogInformation(log.LogResponse());
        }

        private async Task LogRequest(HttpContext httpContext, RequestResponseLog log)
        {
            log.RequestBody = await GetRequestBody(httpContext.Request);

            foreach (var header in httpContext.Request.Headers)
                log.RequestHeaders += header;

            log.RequestPath = httpContext.Request.Path;
            log.RequestHost = httpContext.Request.Host.ToString();
            log.RequestQueryString = httpContext.Request.QueryString.ToString();

            // Log request
            _logger.LogInformation(log.LogRequest());
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;

            return bodyAsText;
        }

        private async Task<string> GetResponseBody(MemoryStream memoryStream, Stream orginalBody)
        {
            memoryStream.Position = 0;
            string responseBody = new StreamReader(memoryStream).ReadToEnd();

            memoryStream.Position = 0;
            await memoryStream.CopyToAsync(orginalBody);

            return responseBody;
        }
    }
}