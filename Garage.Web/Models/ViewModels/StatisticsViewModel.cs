using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class StatisticsViewModel
    {
        [Display(Name = "Revenue:")]
        public double Intakter { get; set; }
        [Display(Name = "Average Parkingtime:")]
        public int GenomsnittligParkeradTid { get; set; }
        [Display(Name = "No of Wheels in Garage:")]
        public int? AntalHjulIGaraget { get; set; }
        [Display(Name = "Number of Airplanes:")]
        public int AntalFlygplan { get; set; }
        [Display(Name = "Number of Motorcycles:")]
        public int AntalMotorcyklar { get; set; }
        [Display(Name = "Number of Buses:")]
        public int AntalBussar { get; set; }
        [Display(Name = "Number of Cars:")]
        public int AntalBilar { get; set; }
        [Display(Name = "Number of Boats:")]
        public int AntalBatar { get; set; }
    }
}
