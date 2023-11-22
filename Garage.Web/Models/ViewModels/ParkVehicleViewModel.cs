using Garage.Web.Validations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage.Web.Models.ViewModels
{
    public class ParkVehicleViewModel
    {
        public IEnumerable<SelectListItem> Vehicles { get; set; } = new List<SelectListItem>();
        [ParkVehicleId]
        public int VehicleId { get; set; }
        public string SSN { get; set; }
    }
}
