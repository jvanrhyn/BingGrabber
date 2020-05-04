using System;
using System.Threading.Tasks;
using BingGrabber.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BingGrabberCLI
{
    class Program
    {
        public static async Task Main(string[] args)
        {
	        var builder = new HostBuilder()
		        .ConfigureServices((context, services) =>
		        {
			        services.AddLogging(configure => configure.AddConsole());
			        services.AddTransient<BingLoader>();
			        services.AddSingleton<IArgumentParser, ArgumentParser>(x =>
				         new ArgumentParser(args));
			        services.AddScoped<IImageCollector, ImageCollector>();
			        services.AddScoped<IImageUriCollector, ImageUriCollector>();
			        services.AddScoped<ICollectorSource, CollectorSource>();
		        });

	        var host = builder.Build();

	        using var serviceScope = host.Services.CreateScope();
	        {
		        var services = serviceScope.ServiceProvider;

		        try
		        {
			        var myService = services.GetRequiredService<BingLoader>();
			        await myService.Run();

			        Console.WriteLine("Success");
		        }
		        catch (Exception ex)
		        {
			        Console.WriteLine($"Error Occured - {ex.Message}");
		        }
	        }
        }
    }
}
