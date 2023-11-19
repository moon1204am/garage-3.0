using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Garage.Domain.Entities
{
    public class Vehicle
    {
        //private Vehicle()
        //{
        //    LicenseNr = null!;
        //    Color = null!;
        //    Brand = null!;
        //    Model = null!;
        //    Wheels = 0;
        //}

        public Vehicle()
        {
            
        }
        public Vehicle(string licenseNr, string color, string brand, string model, int wheels)
        {
            LicenseNr = licenseNr;
            Color = color;
            Brand = brand;
            Model = model;
            Wheels = wheels;

        }
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
        //FK
        public int VehicleTypeId { get; set; }
        public int PersonId { get; set; }
        //Nav prop
        public Person Person { get; set; } = new Person(null, null, null);
        public ICollection<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();
        [Display(Name = "Fordons typ")]
        public VehicleType VehicleType { get; set; } = new VehicleType();


    }
}
