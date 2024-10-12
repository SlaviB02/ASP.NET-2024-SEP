using Microsoft.EntityFrameworkCore.Storage;

namespace SeminarHub.Common
{
    public static class ValidationConstants
    {
        public const int TopicMinLength = 3;
        public const int TopicMaxLength = 100;

        public const int LecturerMinLength = 5;
        public const int LecturerMaxLength = 60;

        public const int DetailsMinLength = 10;
        public const int DetailsMaxLength = 500;

        public const string DateTimeFormat = "dd/MM/yyyy HH:mm";

        public const int DurationMinValue = 30;
        public const int DurationMaxValue = 180;

        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 50;
    }
}
