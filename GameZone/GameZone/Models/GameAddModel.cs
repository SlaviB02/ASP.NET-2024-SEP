using System.ComponentModel.DataAnnotations;
using static GameZone.Common.ValidationConstants;
namespace GameZone.Models
{
    public class GameAddModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        public string? ImageUrl {  get; set; }=null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        
        public string ReleasedOn { get; set; } = null!;

        [Required]
        [Range(1,int.MaxValue)]
        public int GenreId {  get; set; }

        public IEnumerable<GenreViewModel>? Genres { get; set; }
    }
}
