namespace Garage2._0.Services
{
    public interface IValidation
    {
        bool LicenseNumberExists(string licenseNumber);

        bool ParkingSpaceExists(int size);
    }
}