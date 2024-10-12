using System.ComponentModel.DataAnnotations;
using static SeminarHub.Common.ValidationConstants;
namespace SeminarHub.Models
{
    public class SeminarAddModel
    {
        [Required]
        [MinLength(TopicMinLength)]
        [MaxLength(TopicMaxLength)]
        public string Topic { get; set; } = null!;
        [Required]
        [MinLength(LecturerMinLength)]
        [MaxLength(LecturerMaxLength)]
        public string Lecturer { get; set; } = null!;
        [Required]
        [MinLength(DetailsMinLength)]
        [MaxLength(DetailsMaxLength)]
        public string Details {  get; set; } = null!;
        [Required]
        public string DateAndTime { get; set; } = null!;
        [Range(DurationMinValue, DurationMaxValue)]
        public int Duration {  get; set; }
        [Required]
        public int CategoryId {  get; set; }

        public ICollection<CategoryViewModel>? Categories {  get; set; }
    }
}
