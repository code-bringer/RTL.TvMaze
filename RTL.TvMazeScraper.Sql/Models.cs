using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTL.TvMazeScraper.Sql
{
    public sealed class Show
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Cast> Casts { get; set; }
    }

    public sealed class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public ICollection<Cast> Casts { get; set; }
    }

    public sealed class Cast
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public Show Show { get; set; }
    }
}
