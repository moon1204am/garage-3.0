using ActiveLogin.Identity.Swedish.AspNetCore.Validation;
using Garage.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }

        [Display(Name = "Social Security Number"), Required(ErrorMessage = "SSN is required.")]
        //    [RegularExpression(/*@"^(19|20)?[0-9]{6}[-+]?[0-9]{4}$" *//*@"^(\d{6}|\d{8})-\d{4}$"*/ @"^(19|20) ?\d{2}
        //(01|02|03|04|05|06|07|08|09|10|11|12)((0[1-9])|(1|2)[0-9]|(30|31))-\d{4}$", ErrorMessage = "Invalid SSN, it should be 12 digits.")]
        //[RegularExpression(@"^[0-9]{8}[-+]?[0-9]{4}$", ErrorMessage = "Invalid SSN, please enter a new one")]
        //[RegularExpression(@"^(\d{6}|\d{8})-\d{4}$", ErrorMessage = "Invalid SSN, please enter a new one")]
        [PersonalIdentityNumber]
        public string SSN { get; set; } = string.Empty;

        [Display(Name = "Last name"), Required(ErrorMessage = "A last name is required")]

        public string LastName { get; set; } = string.Empty;

        [Display(Name = "First name"), Required(ErrorMessage = "A first name is required.")]

        public string FirstName { get; set; } = string.Empty;
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}
