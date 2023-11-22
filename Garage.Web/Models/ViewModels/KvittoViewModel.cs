using System.ComponentModel.DataAnnotations;


namespace Garage.Web.Models.ViewModels
{
    public class KvittoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Registreringsnummer")]
        public string RegNr { get; set; }

        [Display(Name = "Ankomst tid")]
        public DateTime AnkomstTid { get; set; }

        [Display(Name = "Utcheckning tid")]
        public DateTime UtchecksTid { get; set; }

        [Display(Name = "Total parkeringstid")]
        public string ParkeringsTid { get; set; }

        [Display(Name = "Pris/timme")]
        public int Pris { get; set; }

        [Display(Name = "Total pris")]
        public int TotalPris { get; set; }

        [Display(Name = "Firstname")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Lastname")]
        public string LastName { get; set; } = string.Empty;

    }
}
