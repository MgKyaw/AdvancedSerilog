using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;

namespace AdvancedSerilog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message} {NewLine} {Exception} ";
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.File(Path.Combine("C:Logs", "Test-Log.txt"),
                    rollingInterval: RollingInterval.Day, outputTemplate: outputTemplate)
                    .CreateLogger();
                CreateHostBuilder(args).Build().Run();
            }
            catch
            {
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).UseSerilog();
    }
}
