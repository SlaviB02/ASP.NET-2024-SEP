using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaApp.Common.EntityValidation.Movie;



namespace CinemaApp.Web.ViewModels.Movie
{
    public class AddMovieInputModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public required string Title { get; set; }
        [Required]
        [MinLength(GenreMinLength)]
        [MaxLength(GenreMaxLength)]
        public required string Genre {  get; set; }

        [Required]
        public required string ReleaseDate {  get; set; }

        [Required]
        [Range(DurationMinValue,DurationMaxValue)]
        public int Duration {  get; set; }

        [Required]
        [MinLength(DirectorMinLength)]
        [MaxLength(DirectorMaxLength)]
        public required string Director { get; set; }

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]

        public required string Description { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
