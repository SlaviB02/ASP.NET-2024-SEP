using System.ComponentModel.DataAnnotations;
using static CinemaApp.Common.EntityValidation.Cinema;
namespace CinemaApp.Data.Models
{
    public class Cinema
    {
        [Key]
        public Guid Id { get; set; }=Guid.NewGuid();

        [Required]
        [StringLength(CinemaNameMaxLength)]
        public required string Name { get; set; }

        [Required]
        [StringLength(LocationMaxLength)]
        public required string Location {  get; set; }

        public ICollection<CinemaMovie>CinemaMovies { get; set; }=new HashSet<CinemaMovie>();
    }
}
