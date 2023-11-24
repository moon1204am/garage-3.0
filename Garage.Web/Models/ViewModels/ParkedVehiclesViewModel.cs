using System;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class ParkedVehiclesViewModel
    {
        public int VehicleId { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Vehicle type")]
        public string VehicleType { get; set; }
        [Display(Name = "License number")]
        public string LicenseNr { get; set; }
        [Display(Name = "Parking time")]
        public string ParkingTime { get; set; }
    }
}
