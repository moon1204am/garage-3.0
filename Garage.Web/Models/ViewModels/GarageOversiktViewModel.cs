using System.ComponentModel.DataAnnotations;

namespace Garage3._0.Models.ViewModels
{
    public class GarageOversiktViewModel
    {
        [Display(Name = "Lediga platser")]
        public string LedigaPlatser { get; set; } = string.Empty;
        [Display(Name = "Antal lediga platser")]
        public double AntalLedigaPlatser { get; set; }
    }
}
