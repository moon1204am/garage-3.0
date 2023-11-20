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
        [Display(Name = "Personnummer"), Required(ErrorMessage = "Du måste ange ett personnumer")]
        [RegularExpression(@"^(19|20)?[0-9]{6}[-+]?[0-9]{6}$", ErrorMessage = "Ogiltigt personnummer, det ska vara 12-siffrigt.")]
        public string SSN { get; set; } = string.Empty;
        
        [Display(Name = "Efternamn"), Required(ErrorMessage = "Du måste ange ett efternamn")]
        
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Förnamn"), Required(ErrorMessage = "Du måste ange ett förnamn")]
        
        public string FirstName { get; set; } = string.Empty;
        //Nav prop
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        
    }
}
