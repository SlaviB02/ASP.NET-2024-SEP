using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data
{
    public class CinemaDbContext:DbContext
    {
        public CinemaDbContext()
        {
            
        }

        public CinemaDbContext(DbContextOptions options):base(options)
        {
            
        }
        public required DbSet<Movie> Movies { get; set; }

        public required DbSet<Cinema> Cinemas { get; set; }

        public required DbSet<CinemaMovie>CinemaMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
