using Garage.Domain.Entities;
using Garage.Web.Validations;

namespace Garage.Web.Models.ViewModels
{
    public class VehiclesOverviewViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        [CheckSSN]
        public string SSN { get; set; } = string.Empty;
        public string LicenseNr { get; set; } = string.Empty;
        public VehicleType? VehicleType { get; set; }
        public IEnumerable<ParkedVehiclesViewModel> ParkedVehiclesViewModel { get; set; } = new List<ParkedVehiclesViewModel>();
        public int FreeSpots { get; set; }
    }
}
