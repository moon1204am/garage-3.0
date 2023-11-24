using System.ComponentModel.DataAnnotations;

namespace Garage.Domain.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        [Required]
        [Display(Name = "Registrations number")]
        public string LicenseNr { get; set; } = string.Empty;
        [StringLength(20)]
        public string Color { get; set; } = string.Empty;
        [StringLength(20)]
        public string Brand { get; set; } = string.Empty;
        [StringLength(20)]
        public string Model { get; set; } = string.Empty;
        [Range(0, 12)]
        public int? Wheels { get; set; }
        public int VehicleTypeId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; } = new Person();
        public ICollection<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();
        [Display(Name = "Vehicle type")]
        public VehicleType VehicleType { get; set; }
        [Required]
        public bool IsParked { get; set; }


    }
}
