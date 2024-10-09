using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Models
{
  public class UserMovie
    {
        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }=null!;

        public Guid MovieId { get; set; }
        [ForeignKey(nameof(MovieId))]   
        public Movie Movie { get; set; } =null!;
    }
}
