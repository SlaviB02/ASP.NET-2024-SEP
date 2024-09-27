using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Configuration
{
    public class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> builder)
        {
            builder.HasKey(cm => new { cm.MovieId, cm.CinemaId });

            builder
                .HasOne(c => c.Cinema)
                .WithMany(cm => cm.CinemaMovies);

            builder
                .HasOne(m => m.Movie)
                .WithMany(cm => cm.CinemaMovies);
        }
    }
}
