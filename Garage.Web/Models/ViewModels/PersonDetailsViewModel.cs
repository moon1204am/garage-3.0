using Garage.Domain.Entities;
using Garage.Web.Validations;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class PersonDetailsViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public int PersonId { get; set; }
        [Display(Name = "SSN")]
        public string SSN { get; set; } = string.Empty;
        [Display(Name = "First name")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Last name")]
        public string LastName { get; set; } = string.Empty;
    }
}
