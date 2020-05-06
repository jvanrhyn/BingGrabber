using System;
using System.Threading.Tasks;
using BingGrabber.Shared;
using BingGrabber.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BingGrabberCLI
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine(@"Usage: BingGrabberCLI.exe from=2019-05 to=2020-01 path=c:\temp\");
				return;
			}

			var builder = new HostBuilder()
				.ConfigureServices((context, services) =>
				{
					services.AddLogging(configure => configure.AddConsole());
					services.AddTransient<Startup>();
					services.AddSingleton<IArgumentParser, ArgumentParser>(x =>
						new ArgumentParser(x.GetRequiredService<ILogger<ArgumentParser>>(), args));
					services.AddScoped<IImageCollector, ImageCollector>();
					services.AddScoped<IImageUriCollector, ImageUriCollector>();
					services.AddScoped<ICollectorSource, CollectorSource>();
				});

			await Run(builder);
		}

		private static async Task Run(IHostBuilder builder)
		{
			var host = builder.Build();

			using var serviceScope = host.Services.CreateScope();
			var services = serviceScope.ServiceProvider;
			var logger = host.Services.GetService<ILogger<Startup>>();
			try
			{
				var myService = services.GetRequiredService<Startup>();
				await myService.Run();

				Console.WriteLine("Completed");
			}
			catch (Exception ex)
			{
				logger.LogError(ex,"Error on startup");
				Console.WriteLine($"Error Occured - {ex.Message}");
			}
		}
	}
}
