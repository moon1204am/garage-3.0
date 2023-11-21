using Garage.Domain.Entities;
using Garage3._0.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class PersonDetailsViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public int PersonId { get; set; }
        [Display(Name = "SSN")]
        public string SSN { get; set; } = string.Empty;
        [Display(Name = "Firstname")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Lastname")]
        public string LastName { get; set; } = string.Empty;
   
    }
}
