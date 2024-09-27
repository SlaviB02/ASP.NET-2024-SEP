using CinemaApp.Web.ViewModels.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.ViewModels.Cinema
{
    public class CinemaDetailsViewModel
    {
        public required string Id {  get; set; }

        public required string Name { get; set; }

        public required string Location { get; set; }

        public IList<MovieProgramView> Movies { get; set; }=new List<MovieProgramView>();
    }
}
