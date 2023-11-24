using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class PersonOverViewViewModel
    {
        public int PersonId { get; set; }
        [Display(Name = "Social Security Number")]
        public string SSN { get; set; } = string.Empty;
        [Display(Name = "First name")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Last name")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Number of parked vehicles")]
        public int NumberOfParkedVehicles { get; set; }



    }
}
