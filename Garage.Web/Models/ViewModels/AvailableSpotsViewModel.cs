using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class AvailableSpotsViewModel
    {
        [Display(Name = "Available Parking spots: ")]
        public string AvailableParkingSpots { get; set; } = string.Empty;
    }
}

