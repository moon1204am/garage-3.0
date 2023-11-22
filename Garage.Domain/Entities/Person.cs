using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Garage.Domain.Entities
{
    public class Person
    {
     
    
        public int PersonId { get; set; }
        [Display(Name = "Social Security Number"), Required(ErrorMessage = "SSN is required.")]
        [RegularExpression(@"^(19|20)?[0-9]{6}[-+]?[0-9]{6}$", ErrorMessage = "Invalid SSN, it should be 12 digits.")]
        public string SSN { get; set; } = string.Empty;
        
        [Display(Name = "Last Name"), Required(ErrorMessage = "A last name is required")]
        
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "First Name"), Required(ErrorMessage = "A first name is required.")]
        
        public string FirstName { get; set; } = string.Empty;
        //Nav prop
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        
    }
}
