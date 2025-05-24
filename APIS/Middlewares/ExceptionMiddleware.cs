

using Core.Dtos.Response;
using Core.Interfaces.IServices.SystemIServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;


namespace RentingCars.APIS.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ExceptionMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory,  IHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILoggerService>();

                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    
                    // Alternative way using endpoint metadata
                    var endpoint = context.GetEndpoint();

                    if (endpoint != null)
                    {
                        var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
                        logger.LogError("UnHandeled Exception", ex);
                    }
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var response = new StandardResponse<string>();
                    if (_environment.IsDevelopment())
                    {
                         response = new StandardResponse<string>(ex.InnerException?.Message??ex.Message, false);
                    }
                    else
                    {
                        response = new StandardResponse<string>("A server error occurred.", false);
                    };

                    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    var json = JsonSerializer.Serialize(response, options);

                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}

