using Garage.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }

        [Display(Name = "Social Security Number"), Required(ErrorMessage = "SSN is required.")]
        public string SSN { get; set; } = string.Empty;

        [Display(Name = "Last name"), Required(ErrorMessage = "A last name is required")]

        public string LastName { get; set; } = string.Empty;

        [Display(Name = "First name"), Required(ErrorMessage = "A first name is required.")]

        public string FirstName { get; set; } = string.Empty;
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}
