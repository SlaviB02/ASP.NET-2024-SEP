using Microsoft.AspNetCore.Identity;

namespace GameZone.Models
{
    public class DetailsForGame
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string Genre { get; set; } = null!;  

        public string ReleasedOn {  get; set; } = null!;

        public string Publisher {  get; set; }=null!;

        public string? ImageUrl { get; set; } 

        public int Id {  get; set; }
    }
}
