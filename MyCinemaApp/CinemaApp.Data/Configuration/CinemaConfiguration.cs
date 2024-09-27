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
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.HasData(SeedCinemas());
        }
        private List<Cinema>SeedCinemas()
        {
            List<Cinema> cinemas = new List<Cinema>()
            {
                new Cinema()
                {
                    Name="Cinema City",
                    Location="Sofia"
                },
                new Cinema()
                {
                    Name="Cinema City",
                    Location="Burgas"
                },
                new Cinema()
                {
                    Name="Imax",
                    Location="Plovdiv"
                }
            };
            return cinemas;
        }
    }
}
