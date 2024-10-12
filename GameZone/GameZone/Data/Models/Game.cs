using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameZone.Common.ValidationConstants;

namespace GameZone.Data.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength)]

        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public string? ImageUrl {  get; set; }
        [Required]
        public string PublisherId {  get; set; }=null!;
       
        [ForeignKey(nameof(PublisherId))]
        public IdentityUser Publisher { get; set; }=null!;

        [Required]
        public DateTime ReleasedOn { get; set; }

        [Required]
        public int GenreId {  get; set; }

        
        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; } =null!;

        public ICollection<GamerGame> GamersGames { get; set; } = new HashSet<GamerGame>();

    }
}
