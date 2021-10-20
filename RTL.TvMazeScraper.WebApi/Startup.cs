using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RTL.TvMazeScraper.Core;
using RTL.TvMazeScraper.Sql;

namespace RTL.TvMazeScraper.WebApi
{
    public class Startup
    {
        private const uint PageSize = 100;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ITvMazeDbContext, TvMazeDbContext>();
            services.AddTransient<IShowSelector, ShowRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment _)
        {
            app.UseRouting().UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/api/show", async context =>
                {
                    uint.TryParse(context.Request.Query["page"], out var page);
                    var selector = context.RequestServices.GetRequiredService<IShowSelector>();
                    var shows = await selector.GetShows(page, PageSize);
                    await context.Response.WriteAsJsonAsync(shows);
                });
            });
        }
    }
}
