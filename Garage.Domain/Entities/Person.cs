using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string SSN { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        //Nav prop
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        
    }
}
