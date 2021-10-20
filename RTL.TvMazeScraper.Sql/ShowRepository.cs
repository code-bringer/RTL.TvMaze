using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RTL.TvMazeScraper.Core;

namespace RTL.TvMazeScraper.Sql
{
    public sealed class ShowRepository : IShowImporter, IShowSelector
    {
        private readonly ITvMazeDbContext _context;

        public ShowRepository(ITvMazeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddShow(Core.Show show)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            await _context.Show.AddAsync(new Show
            {
                Id = show.Id,
                Name = show.Name
            }).ConfigureAwait(false);

            foreach (var person in show.Casts)
            {
                if (await _context.Person.FindAsync(person.Id).ConfigureAwait(false) == null)
                {
                    await _context.Person.AddAsync(new Person
                    {
                        Birthday = person.Birthday,
                        Id = person.Id,
                        Name = person.Name
                    }).ConfigureAwait(false);
                }

                await _context.Cast.AddAsync(new Cast
                {
                    ShowId = show.Id,
                    PersonId = person.Id
                }).ConfigureAwait(false);
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task<Core.Show[]> GetShows(uint page, uint pageSize)
        {
            var query = 
                _context.Show
                    .Include(s => s.Casts)
                    .ThenInclude(c => c.Person)
                    .OrderBy(s => s.Id)
                    .Skip((int)(page * pageSize))
                    .Take((int)pageSize)
                    .Select(s => new Core.Show
                    {
                        Name = s.Name,
                        Id = s.Id,
                        Casts = s.Casts.Select(c => new Core.Person
                        {
                            Birthday = c.Person.Birthday,
                            Id = c.Person.Id,
                            Name = c.Person.Name
                        }).OrderByDescending(p => p.Birthday).ToArray()
                    });

            return query.ToArrayAsync();
        }
    }
}