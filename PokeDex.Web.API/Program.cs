using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace PokeDex.Web.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
	        Log.Logger = new LoggerConfiguration()
		        .MinimumLevel.Debug()
		        .WriteTo.File("logs/pokedex-api.txt", rollingInterval: RollingInterval.Day)
		        .CreateLogger();
	        
	        Log.Information("PokeDex.Web.API Started");

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
