using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RTL.TvMazeScraper.Sql
{
    public interface ITvMazeDbContext
    {
        DbSet<Show> Show { get; }
        DbSet<Person> Person { get; }
        DbSet<Cast> Cast { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }

    public sealed class TvMazeDbContext : DbContext, ITvMazeDbContext
    {
        public DbSet<Show> Show { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Cast> Cast { get; set; }
        public string DbPath { get; }

        public TvMazeDbContext(string dbPath = null)
        {
            DbPath = dbPath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RTL", "tvMaze.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => 
            options.UseSqlite($"Data Source={DbPath}")
                .LogTo(Console.WriteLine)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Show>().ToTable("Show");
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Cast>().ToTable("Cast");
        }
    }
}