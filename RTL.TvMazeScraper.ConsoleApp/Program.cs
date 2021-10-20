using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RTL.TvMazeScraper.Core;
using RTL.TvMazeScraper.Sql;
using TvMaze.Api.Client;
using Show = TvMaze.Api.Client.Models.Show;

namespace RTL.TvMazeScraper.ConsoleApp
{
    public static class Program
    {
        public static async Task Main()
        {
            await using var context = new TvMazeDbContext();
            await context.Database.EnsureCreatedAsync();

            var client = new TvMazeClient();

            IShowImporter showRepository = new ShowRepository(context);

            int page = 0;
            IEnumerable<Show> shows = await client.Shows.GetShowsAsync(page);
            
            while (shows.Any())
            {
                foreach (var show in shows)
                {
                    var casts = await client.Shows.GetShowCastAsync(show.Id);
                    await showRepository.AddShow(new Core.Show
                    {
                        Name = show.Name,
                        Id = show.Id,
                        Casts = casts.Select(c => new Core.Person
                        {
                            Name = c.Person.Name,
                            Birthday = c.Person.Birthday,
                            Id = c.Person.Id
                        }).ToArray()
                    });
                }

                page++;
                shows = await client.Shows.GetShowsAsync(page);
            }
        }
    }
}
