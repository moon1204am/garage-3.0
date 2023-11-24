namespace Garage.Domain.Entities
{
    public class ParkingSpot
    {
        public int ParkingSpotId { get; set; }
        public DateTime Arrival { get; set; }
        public int? VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        
    }
}
