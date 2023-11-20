using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Domain.Entities
{
    public class VehicleType
    {
        public int VehicleTypeId { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Size { get; set; } 
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
