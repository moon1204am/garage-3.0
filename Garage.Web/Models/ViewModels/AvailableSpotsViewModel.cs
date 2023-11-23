using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class AvailableSpotsViewModel
    {
        [Display(Name = "Available Parkingspots: ")]
        public string AvailableParkingSpots { get; set; } = string.Empty;
    }
}

