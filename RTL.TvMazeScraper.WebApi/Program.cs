using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace RTL.TvMazeScraper.WebApi
{
    public static class Program
    {
        public static Task Main(string[] args) => 
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .Build()
            .RunAsync();
    }
}
