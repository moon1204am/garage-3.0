using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Domain.Entities
{
    public class ParkingSpot
    {
        public int ParkingSpotId { get; set; }
        public DateTime Arrival { get; set; }
        //FK
        public int? VehicleId { get; set; }
        //Nav prop
        public Vehicle Vehicle { get; set; }
        
    }
}
