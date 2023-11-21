using Garage.Domain.Entities;
using Garage2._0.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Models.ViewModels
{
    public class CreateVehicleViewModel
    {
        public int VehicleId { get; set; }
        public int VehicleTypeId { get; set; }
        //[Required]
        //[Display(Name = "Vehicle type")]
        //public VehicleType VehicleType { get; set; } = new VehicleType();
        [Required]
        [RegularExpression(@"[A-Za-z]{3}[0-9]{2}[A-Za-z0-9]{1}", ErrorMessage = "Please provide a valid license number.")]
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
        //[Required]
        //public IEnumerable<SelectListItem> VehicleTypes { get; set; } = new List<SelectListItem>();

        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        [Required]
        [CheckSSN]
        [Display(Name = "Social security number")]
        public string PersonSSN { get; set; } = string.Empty;

    }
}
