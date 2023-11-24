using Garage.Web.Validations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class ParkVehicleViewModel
    {
        public IEnumerable<SelectListItem> Vehicles { get; set; } = new List<SelectListItem>();
        [ParkVehicleId]
        [Display(Name = "Choose vehicle")]
        public int VehicleId { get; set; }
        [Display(Name = "Social security number")]
        public string SSN { get; set; }
    }
}
