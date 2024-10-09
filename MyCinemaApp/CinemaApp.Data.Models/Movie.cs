using System.ComponentModel.DataAnnotations;
using static CinemaApp.Common.EntityValidation.Movie;
namespace CinemaApp.Data.Models
{
    public class Movie
    {
        public Movie()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(TitleMaxLength)]
        public required string Title { get; set; }

        [Required]
        [StringLength(GenreMaxLength)]
        public required string Genre {  get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [StringLength (DirectorMaxLength)]
        public required string Director {  get; set; }

        [Required]
        [Range(1,500)]
        public int Duration {  get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]

        public required string Description { get; set; }

        public string? ImageUrl {  get; set; }

        public ICollection<CinemaMovie> CinemaMovies { get; set; } = new HashSet<CinemaMovie>();

        public ICollection<UserMovie> MovieUsers { get; set; }=new HashSet<UserMovie>();

    }
}
