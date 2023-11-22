
using Garage.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage3._0.Models.ViewModels
{
    public class PersonOverViewViewModel
    {
        public int PersonId { get; set; }
        [Display(Name = "Personnummer")]
        public string SSN { get; set; } = string.Empty;
        [Display(Name = "Firstname")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Lastname")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Antal Parkerade Fordon")]
        public int NumberOfParkedVehicles { get; set; }


       
    }
}
