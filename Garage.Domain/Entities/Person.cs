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
        //private Person()
        //{
        //    FirstName = null!;
        //    LastName = null!;
        //    SSN = null!;
        //}
        public Person()
        {
            
        }
        public Person(string firstName, string lastName, string SSN)
        {
            FirstName = firstName;
            LastName = lastName;
            this.SSN = SSN;
        }
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Du måste ange ett personnumer")]
        [Display(Name = "Personnummer")]
        public string SSN { get; set; } = string.Empty;
        [Required(ErrorMessage = "Du måste ange ett förnamn")]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Du måste ange ett efternamn")]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; } = string.Empty;
        //Nav prop
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        
    }
}
