using Garage.Domain.Entities;
using Garage2._0.Validations;

namespace Garage2._0.Models.ViewModels
{
    public class VehiclesOverviewViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        [CheckSSN]
        public string SSN { get; set; } = string.Empty;
    }
}
