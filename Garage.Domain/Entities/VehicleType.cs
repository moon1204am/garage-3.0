using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Domain.Entities
{
    public class VehicleType
    {
        public int VehicleTypeId { get; set; }
        [StringLength(20)]
        public string Type { get; set; } = string.Empty;
        [Range(1, 3)]
        public int Size { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
