using Microsoft.AspNetCore.Identity;

namespace GameZone.Models
{
    public class DeleteGameModel
    {
        public string Title { get; set; } = null!;

        public string Publisher { get; set; } = null!;

        public int Id { get; set; }
    }
}
