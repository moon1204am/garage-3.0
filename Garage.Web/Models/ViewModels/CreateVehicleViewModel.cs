using Garage.Domain.Entities;
using Garage.Web.Validations;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class CreateVehicleViewModel
    {
        public int VehicleId { get; set; }
        [Required]
        [Display(Name = "Vehicle type")]
        public int VehicleTypeId { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z]{3}[0-9]{2}[A-Za-z0-9]{1}", ErrorMessage = "Please provide a valid license number.")]
        [CheckLicenseNumber]
        [Display(Name = "License number")]
        public string LicenseNr { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string Color { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        [Display(Name = "Brand")]
        public string Brand { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        [Display(Name = "Model")]
        public string Model { get; set; } = string.Empty;
        [Required]
        [Range(0, 12)]
        public int? Wheels { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        [Required]
        [CheckSSN]
        [Display(Name = "Social security number")]
        public string PersonSSN { get; set; } = string.Empty;

    }
}
