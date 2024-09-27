using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.ViewModels.Cinema
{
    public class CinemaCheckBoxItem
    {
        public required string Id {  get; set; }

        public required string Name {  get; set; }

        public required string Location { get; set; }

        public bool IsSelected {  get; set; }
    }
}
