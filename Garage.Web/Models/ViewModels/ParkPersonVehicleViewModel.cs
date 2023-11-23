using Garage.Web.Validations;

namespace Garage.Web.Models.ViewModels
{
    public class ParkPersonVehicleViewModel
    {
        public ParkVehicleViewModel ParkVehicleViewModel { get; set; }
        public PersonDetailsViewModel PersonDetailsViewModel { get; set; }
        [ParkVehicleId]
        public int VehicleId { get; set; }

    }
}
