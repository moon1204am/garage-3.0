using System.ComponentModel.DataAnnotations;

namespace Garage.Domain.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        [Required]
        [Display(Name = "Registreringsnummer")]
        public string LicenseNr { get; set; } = string.Empty;
        [StringLength(20)]
        [Display(Name = "Färg")]
        public string Color { get; set; } = string.Empty;
        [StringLength(20)]
        [Display(Name = "Märke")]
        public string Brand { get; set; } = string.Empty;
        [StringLength(20)][Display(Name = "Modell")] 
        public string Model { get; set; } = string.Empty;
        [Display(Name = "Antal hjul")]
        [Range(0, 12)]
        public int? Wheels { get; set; }
        [Display(Name = "Ankomst tid")]
        public DateTime Arrival { get; set; }
        [Required]
        public int ParkingIndex { get; set; }
        //FK
        public int VehicleTypeId { get; set; }
        public int PersonId { get; set; }
        //Nav prop
        public Person Person { get; set; } = new Person();
        public ICollection<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();
        [Display(Name = "Fordons typ")]
        public VehicleType VehicleType { get; set; } = new VehicleType();


    }
}
