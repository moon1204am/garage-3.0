using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class PersonIndexViewModel
    {
        public IEnumerable<PersonOverViewViewModel> Members { get; set; } = new List<PersonOverViewViewModel>();
        [Display(Name = "Last name")]
        public string LastName { get; set; } = string.Empty;
        public int PersonId { get; set; }



    }
}
