using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_2.Models
{
    public class MovieLibraryContext: DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonFilm> PersonFilms { get; set; }
        public virtual DbSet<PersonSeries> PersonSeries { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public MovieLibraryContext(DbContextOptions<MovieLibraryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
