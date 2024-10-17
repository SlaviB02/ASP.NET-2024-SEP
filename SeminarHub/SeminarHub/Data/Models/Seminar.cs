using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeminarHub.Common.ValidationConstants;
namespace SeminarHub.Data.Models
{
    public class Seminar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TopicMaxLength)]

        public string Topic { get; set; } = null!;

        [Required]
        [MaxLength(LecturerMaxLength)]
        public string Lecturer {  get; set; } = null!;

        [Required]
        [MaxLength(DetailsMaxLength)]
        public string Details { get; set; } = null!;

        [Required]
        public string OrganizerId {  get; set; }=null!;

        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        public DateTime DateAndTime { get; set; }

       
        public int Duration {  get; set; }
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public ICollection<SeminarParticipant> SeminarsParticipants { get; set; } = new HashSet<SeminarParticipant>();

        public bool IsDeleted {  get; set; }
    }
}
