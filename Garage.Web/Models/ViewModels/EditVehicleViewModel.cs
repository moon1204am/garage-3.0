using Garage.Domain.Entities;
using Garage.Web.Validations;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class EditVehicleViewModel
    {
        public int VehicleId { get; set; }
        [Required]
        [StringLength(20)]
        public string Color { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string Brand { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string Model { get; set; } = string.Empty;
        [Required]
        [Range(0, 12)]
        public int? Wheels { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
