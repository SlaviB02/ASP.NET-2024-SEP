using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.ViewModels.Cinema
{
    public class AddMovieToCinemaProgram
    {
        public required string MovieId {  get; set; }

        public required string MovieTitle {  get; set; }


        public IList<CinemaCheckBoxItem> Cinemas { get; set; }=new List<CinemaCheckBoxItem>();
    }
}
