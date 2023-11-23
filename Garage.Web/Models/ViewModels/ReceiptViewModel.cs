using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class ReceiptViewModel
    {
        public int VehicleId { get; set; }
        public int ParkingSpotID { get; set; }

        [Display(Name = "License number")]
        public string LicenseNr { get; set; }
        
        public  string Name { get; set; }

        public DateTime Arrival { get; set; }

        [Display(Name = "Check out")]
        public DateTime CheckOut { get; set; }

        [Display(Name = "Parking time")]
        public string ParkingTime { get; set; }

        [Display(Name = "Price/hour")]
        public int Price { get; set; }

        [Display(Name = "Total cost")]
        public int TotalCost { get; set; }

       
    }
}
