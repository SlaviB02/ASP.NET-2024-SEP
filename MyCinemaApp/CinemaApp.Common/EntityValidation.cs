using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Common
{
   public static class EntityValidation
    {
        public static class Movie
        {
            public const int TitleMinLength = 1;
            public const int TitleMaxLength = 50;
            public const int GenreMaxLength = 20;
            public const int GenreMinLength = 5;
            public const int DirectorMinLength = 5;
            public const int DirectorMaxLength = 50;
            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 400;
            public const int DurationMinValue = 1;
            public const int DurationMaxValue = 999;
        }
        public static class Cinema
        {
            public const int CinemaNameMinLength = 3;
            public const int CinemaNameMaxLength = 50;
            public const int LocationMinLength = 3;
            public const int LocationMaxLength = 85;
        }
    }
}
