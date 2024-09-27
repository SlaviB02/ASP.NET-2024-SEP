using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Models
{
    public class CinemaMovie
    {
        public Guid CinemaId { get; set; }
        [ForeignKey(nameof(CinemaId))]
        public Cinema Cinema { get; set; } = null!;

        public Guid MovieId { get; set; }
        [ForeignKey(nameof(MovieId))]
        public Movie Movie { get; set; } = null!;
    }
}
