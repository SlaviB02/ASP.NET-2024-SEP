using System.ComponentModel.DataAnnotations;

using static CinemaApp.Common.EntityValidation.Cinema;
namespace CinemaApp.Web.ViewModels.Cinema
{
    public class CinemaCreateViewModel
    {
        [Required]
        [MinLength(CinemaNameMinLength)]
        [MaxLength(CinemaNameMaxLength)]
        public required string Name {  get; set; }

        [Required]
        [MinLength(LocationMinLength)]
        [MaxLength(LocationMaxLength)]
        public required string Location {  get; set; }
    }
}
