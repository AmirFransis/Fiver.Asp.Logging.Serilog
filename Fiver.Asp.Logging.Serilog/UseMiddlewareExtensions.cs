using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Fiver.Asp.Logging.Serilog
{
    public static class UseMiddlewareExtensions
    {
        public static IApplicationBuilder UseHelloLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HelloLoggingMiddleware>();
        }
    }

    public class HelloLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<HelloLoggingMiddleware> logger;

        public HelloLoggingMiddleware(
            RequestDelegate next,
            ILogger<HelloLoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var message = new
            {
                GreetingTo = "James Bond",
                GreetingTime = "Morning",
                GreetingType = "Good"
            };
            this.logger.LogInformation("Inoke executing {@message}", message);
            
            await context.Response.WriteAsync("Hello Logging!");
            
            this.logger.LogInformation(
                "Inoke executed by {developer} at {time}", "Tahir", DateTime.Now);
        }
    }
}
