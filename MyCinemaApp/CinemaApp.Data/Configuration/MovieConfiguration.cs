using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaApp.Common.ApplicationConstants;

namespace CinemaApp.Data.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasData(SeedMovies());

            builder.Property(i => i.ImageUrl)
                .IsRequired(false)
                .HasDefaultValue(defaultUrlImage);
        }

        private List<Movie> SeedMovies()
        {
            List<Movie> movies = new List<Movie>()
            {
                 new Movie()
                 {
                     
                     Title="The Fall Guy",
                     Genre="Action",
                     ReleaseDate=new DateTime(2024,05,03),
                     Director="David Leech",
                     Duration=125,
                     Description="The Fall Guy is a 2024 American action comedy film directed by David Leitch and written by Drew Pearce, loosely based on the 1980s TV series. The plot follows a stuntman (Ryan Gosling) working on his ex-girlfriend's (Emily Blunt) directorial debut action film, only to find himself involved in a conspiracy surrounding the film's lead actor (Aaron Taylor-Johnson). "
                 }
                 ,
                 new Movie()
                 {
                    
                     Title="Deadpool & Wolverine",
                     Genre="Action",
                     ReleaseDate=new DateTime(2024,07,25),
                     Director="Shaun Levi",
                     Duration=127,
                     Description="Deadpool & Wolverine is a 2024 American superhero film based on Marvel Comics featuring the characters Deadpool and Wolverine. Produced by Marvel Studios, Maximum Effort, and 21 Laps Entertainment, and distributed by Walt Disney Studios Motion Pictures, it is the 34th film in the Marvel Cinematic Universe (MCU) and the sequel to Deadpool (2016) and Deadpool 2 (2018). "
                 }
            };
            return movies;
        }
    }
}
