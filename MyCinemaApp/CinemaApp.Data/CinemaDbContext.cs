using CinemaApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data
{
    public class CinemaDbContext:IdentityDbContext<IdentityUser>
    {
        public CinemaDbContext()
        {
            
        }

        public CinemaDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Movie> Movies { get; set; } = null!;

        public DbSet<Cinema> Cinemas { get; set; } = null!;

        public DbSet<CinemaMovie> CinemaMovies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
