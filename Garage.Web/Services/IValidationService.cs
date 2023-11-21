using Garage.Domain.Entities;

namespace Garage2._0.Services
{
    public interface IValidationService
    {
        IEnumerable<ParkingSpot> FoundParkingSpot { get; set; }

        int GetSizeFromType(int input);
        public int GetSizeFromId(int id);
        bool LicenseNumberExists(string licenseNumber);
        bool SSNExists(string SSN);

        bool ParkingSpaceExists(int size);
    }
}