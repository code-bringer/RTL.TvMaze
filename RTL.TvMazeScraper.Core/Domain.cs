using System;
using System.Threading.Tasks;

namespace RTL.TvMazeScraper.Core
{
    public sealed class Show
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Person[] Casts { get; set; } = Array.Empty<Person>();
    }

    public sealed class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? Birthday { get; set; }
    }

    public interface IShowImporter
    {
        Task AddShow(Show show);
    }

    public interface IShowSelector
    {
        Task<Show[]> GetShows(uint page, uint pageSize);
    }
}
