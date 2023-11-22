using Garage.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class StartsidaViewModel
    {
        public IEnumerable<FordonOversiktViewModel> ParkeradeFordon { get; set; } = new List<FordonOversiktViewModel>();
        [Display(Name = "Antal lediga platser")]
        public double AntalLedigaPlatser { get; set; }
        public VehicleType? FordonsTyp { get; set; }
        public string? RegNr { get; set; }
    }
}
