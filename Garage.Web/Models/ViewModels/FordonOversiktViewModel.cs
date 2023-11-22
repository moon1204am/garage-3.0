using Garage.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Models.ViewModels
{
    public class FordonOversiktViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Fordonstyp")]
        public VehicleType? FordonsTyp { get; set; }
        [Display(Name = "Registreringsnummer")]
        public string RegNr { get; set; } = string.Empty;
        [Display(Name = "Ankomsttid")]
        public DateTime AnkomstTid { get; set; }
    }
}