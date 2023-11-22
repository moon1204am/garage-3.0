using Garage3._0.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class PersonIndexViewModel
    {
        public IEnumerable<PersonOverViewViewModel> Members { get; set; } = new List<PersonOverViewViewModel>();
        [Display(Name = "Efternamn")]
        public string LastName { get; set; } = string.Empty;
        public int PersonId { get; set; }

        public int PageSize { get; set; }



    }
}
