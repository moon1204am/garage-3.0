using Bogus.DataSets;
using Garage2._0.Validations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage2._0.Models.ViewModels
{
    public class ParkVehicleViewModel
    {
        public IEnumerable<SelectListItem> Vehicles { get; set; } = new List<SelectListItem>();
        [ParkVehicleId]
        public int VehicleId { get; set; }
        public string SSN { get; set; }
    }
}
