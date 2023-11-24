using Garage.Web.Validations;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class VehiclesOverviewViewModel
    {
        [CheckSSN]
        [Display(Name = "Social security number")]
        public string SSN { get; set; } = string.Empty;
        [Display(Name = "License number")]
        public string LicenseNr { get; set; } = string.Empty;
        public int? VehicleTypeId { get; set; }
        public IEnumerable<ParkedVehiclesViewModel> ParkedVehiclesViewModel { get; set; } = new List<ParkedVehiclesViewModel>();
        public int FreeSpots { get; set; }
    }
}
