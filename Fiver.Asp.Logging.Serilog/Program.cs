using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Fiver.Asp.Logging.Serilog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((context, builder) =>
                {
                    builder.ClearProviders(); // to remove default (console, debug) providers

                    builder.AddFilter((category, level) =>
                        category.Contains("Fiver.Asp.Logging.Serilog.HelloLoggingMiddleware"));

                    //Log.Logger = new LoggerConfiguration()
                    //            .WriteTo.LiterateConsole()
                    //            .CreateLogger();

                    //Log.Logger = new LoggerConfiguration()
                    //            .Enrich.WithProperty("ApiVersion", "1.2.5000")
                    //            .WriteTo.LiterateConsole()
                    //            .CreateLogger();

                    //Log.Logger = new LoggerConfiguration()
                    //            .Enrich.WithProperty("ApiVersion", "1.2.5000")
                    //            .WriteTo.LiterateConsole()
                    //            .CreateLogger()
                    //            .ForContext<HelloLoggingMiddleware>();

                    Log.Logger = new LoggerConfiguration()
                                .Enrich.WithProperty("ApiVersion", "1.2.5000")
                                .WriteTo.LiterateConsole()
                                .WriteTo.Seq("http://localhost:5341")
                                .CreateLogger()
                                .ForContext<HelloLoggingMiddleware>();
                })
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}
