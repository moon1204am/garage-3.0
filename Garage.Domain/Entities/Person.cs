using System.ComponentModel.DataAnnotations;


namespace Garage.Domain.Entities
{
    public class Person
    {
     
        public int PersonId { get; set; }
        
        public string SSN { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        
    }
}
