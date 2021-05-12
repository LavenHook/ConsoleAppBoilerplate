using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppBoilerplate
{
  public class Startup : IHostedService
  {
    private ILogger<Startup> Logger { get; }
    public IHostEnvironment Environment { get; }
    public IServiceProvider ServiceProvider { get; }

    private readonly IHostApplicationLifetime AppLifetime;
    private CancellationTokenSource CancellationTokenSource { get; } = new CancellationTokenSource();
    private TaskCompletionSource<bool> TaskCompletionSource { get; } = new TaskCompletionSource<bool>();
    private StartupOptions Options { get; set; }

    public Startup(ILogger<Startup> logger, IHostApplicationLifetime appLifetime, IOptions<StartupOptions> options, IHostEnvironment environment, IServiceProvider serviceProvider)
    {
      Logger = logger;
      AppLifetime = appLifetime;
      Environment = environment;
      ServiceProvider = serviceProvider;
      Options = options?.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {

      AppLifetime.ApplicationStarted.Register(OnStarted);
      AppLifetime.ApplicationStopping.Register(OnStopping);
      AppLifetime.ApplicationStopped.Register(OnStopped);


      //Task.Run(() => Run(CancellationTokenSource.Token));
      await Run(CancellationTokenSource.Token);
      TaskCompletionSource.SetResult(true);
      AppLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      CancellationTokenSource.Cancel();
      // Defer completion promise, until our application has reported it is done.
      return Task.CompletedTask;
    }

    public async Task Run(CancellationToken cancellationToken)
    {
      //TODO: add execution code here
      await Task.CompletedTask;
    }

    private void OnStarted()
    {
      Logger.LogInformation("OnStarted has been called.");

      // Perform post-startup activities here
    }

    private void OnStopping()
    {
      Logger.LogInformation("OnStopping has been called.");

      // Perform on-stopping activities here
    }

    private void OnStopped()
    {
      Logger.LogInformation("OnStopped has been called.");

      // Perform post-stopped activities here
    }
  }
}
