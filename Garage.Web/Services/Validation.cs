using Garage.Data.Data;
using Garage.Domain.Entities;
using NuGet.Packaging.Signing;

namespace Garage2._0.Services
{
    public class Validation : IValidation
    {
        private readonly Garage2_0Context db;

        public Validation(Garage2_0Context db)
        {
            this.db = db;
        }
        public bool LicenseNumberExists(string licenseNumber)
        {
            return false;
        }

        public bool ParkingSpaceExists(int size)
        {
            IEnumerable<ParkingSpot> foundParkingSpot = new List<ParkingSpot>();
            var parkingSpots = db.ParkingSpot.ToList();

            foreach (var parkingSpot in parkingSpots) 
            {
                var spots = parkingSpots.Take(size);
                var consecutives = FindConsecutiveParkingSpots(spots, size);
                if(consecutives != null)
                {
                    foundParkingSpot = consecutives;
                    return true;
                }
                spots.Skip(1);
            }
            return false;
            //var currentParkingSpot = parkingSpots.Select(p => p.VehicleId)
            //                                     .Aggregate((accumulator, current) =>
            //                                        current == null ? accumulator + 1 : 0
            //                                     );
        }

        private IEnumerable<ParkingSpot> FindConsecutiveParkingSpots(IEnumerable<ParkingSpot> spots, int size)
        {
            if (spots.Where(p => p.VehicleId == null).Count() >= size)
            {
                return spots;
            }
            return null;
        }
    }
}
