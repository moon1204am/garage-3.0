﻿using Garage.Data;
using Garage.Data.Data;
using Garage.Domain.Entities;

namespace Garage.Web.Services
{
    public class ValidationService : IValidationService
    {
        private readonly Garage2_0Context db;

        public ValidationService(Garage2_0Context db)
        {
            this.db = db;
        }

        public IEnumerable<ParkingSpot> FoundParkingSpot { get; set; }

        public int GetSizeFromType(int typeId)
        {
            switch (typeId)
            {
                case 1:
                case 2:
                    return GarageSettings.large;
                case 3:
                    return GarageSettings.medium;
                case 4:
                    return GarageSettings.normal;
                case 5:
                    return GarageSettings.small;
                default:
                    break;
            }
            return -1;
        }

        public int GetSizeFromId(int id)
        {
            var type = db.Vehicle.Where(v => v.VehicleId == id).Select(v => v.VehicleTypeId).FirstOrDefault();

            return GetSizeFromType(type);
        }

        public bool LicenseNumberExists(string licenseNumber)
        {
            return false;
        }

        public bool ParkingSpaceExists(int size)
        {
            var parkingSpots = db.ParkingSpot.ToList();
            int n = 0;
            foreach (var parkingSpot in parkingSpots)
            {
                var spots = parkingSpots.Skip(n).Take(size);
                var consecutives = FindConsecutiveParkingSpots(spots, size);
                if (consecutives != null)
                {
                    FoundParkingSpot = consecutives;
                    return true;
                }
                n++;
            }
            return false;
        }

        private IEnumerable<ParkingSpot> FindConsecutiveParkingSpots(IEnumerable<ParkingSpot> spots, int size)
        {
            if (spots.Where(p => p.VehicleId == null).Count() >= size)
            {
                return spots;
            }
            return null;
        }

        public bool SSNExists(string SSN)
        {
            return db.Person.Any(p => p.SSN == SSN);
        }

        public bool LicenseNrExists(string licenseNr)
        {
            return db.Vehicle.FirstOrDefault(p => p.LicenseNr == licenseNr) != null;
        }
    }
}
