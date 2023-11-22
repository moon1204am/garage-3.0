using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Garage.Domain.Entities
{
    public class Vehicle
    {

        public int VehicleId { get; set; }
        [Required]
        [Display(Name = "Registrations number")]
        public string LicenseNr { get; set; } = string.Empty;
        [StringLength(20)]
        [Display(Name = "Color")]
        public string Color { get; set; } = string.Empty;
        [StringLength(20)]
        [Display(Name = "Brand")]
        public string Brand { get; set; } = string.Empty;
        [StringLength(20)][Display(Name = "Model")] 
        public string Model { get; set; } = string.Empty;
        [Display(Name = "Wheels")]
        [Range(0, 12)]
        public int? Wheels { get; set; }
        //FK
        public int VehicleTypeId { get; set; }
        public int PersonId { get; set; }
        //Nav prop
        public Person Person { get; set; } = new Person();
        public ICollection<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();
        [Display(Name = "Vehicle type")]
        public VehicleType VehicleType { get; set; } = new VehicleType();


    }
}
