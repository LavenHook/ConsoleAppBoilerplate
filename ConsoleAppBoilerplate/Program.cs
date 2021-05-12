using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleAppBoilerplate
{
  class Program
  {
    static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureLogging(builder =>
        {
          //builder.ClearProviders();
          //builder.SetMinimumLevel(LogLevel.Error);
        })
        .ConfigureAppConfiguration((Context, Builder) =>
        {
          Builder.AddJsonFile($"{typeof(Program).Namespace}.json", optional: false);
        })
        .ConfigureHostConfiguration(configHost =>
        {
          configHost.SetBasePath(Directory.GetCurrentDirectory());
          //TODO: add environment settings file
          configHost.AddEnvironmentVariables();
          configHost.AddCommandLine(args);
        })
        .ConfigureServices((hostContext, services) =>
        {
          //TODO: add Dependencies here
          services.Configure<StartupOptions>(hostContext.Configuration.GetSection("StartupOptions"));
          services.AddHostedService<Startup>();
        });
  }
}