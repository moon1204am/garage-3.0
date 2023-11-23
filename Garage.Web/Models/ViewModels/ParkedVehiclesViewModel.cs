using System;

namespace Garage.Web.Models.ViewModels
{
    public class ParkedVehiclesViewModel
    {
        public int VehicleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VehicleType { get; set; }
        public string LicenseNr { get; set; }
        public string ParkingTime { get; set; }
    }
}
