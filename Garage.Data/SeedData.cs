using Bogus.DataSets;
using Bogus;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Garage.Data.Data;
using Microsoft.EntityFrameworkCore;
using Bogus.Extensions.Sweden;
using Garage.Domain.Entities;
using Person = Garage.Domain.Entities.Person;
using System.ComponentModel.DataAnnotations;
using Bogus.Extensions.UnitedKingdom;

namespace Garage.Data
{
    public class SeedData
    {
        private static Faker faker = null!;
        public static async Task InitAsync(Garage2_0Context db)
        {
            if (await db.Person.AnyAsync()) return;

            faker = new Faker("sv");

            var persons = GeneratePersons(10);
            await db.AddRangeAsync(persons);

            var types = GenerateVehicleTypes();
            await db.AddRangeAsync(types);

            var vehicles = GenerateVehicles(persons, types);
            await db.AddRangeAsync(vehicles);

            await db.SaveChangesAsync();
        }

        private static IEnumerable<Person> GeneratePersons(int numberOfPersons)
        {
            var students = new List<Person>();

            for (int i = 0; i < numberOfPersons; i++)
            {
                      
                 var person = new Person
                    {
                         FirstName = faker.Name.FirstName(),
                         LastName = faker.Name.LastName(),
                         SSN = faker.Person.Personnummer()
                    };

            students.Add(person);
            }

            return students;
        }

        private static IEnumerable<VehicleType> GenerateVehicleTypes()
        {
            var vehicleTypes = new List<VehicleType>
            {
                new VehicleType { Type = GarageSettings.airplane, Size = GarageSettings.large },
                new VehicleType { Type = GarageSettings.boat, Size = GarageSettings.large },
                new VehicleType { Type = GarageSettings.bus, Size = GarageSettings.medium },
                new VehicleType { Type = GarageSettings.car, Size = GarageSettings.normal },
                new VehicleType { Type = GarageSettings.motorcycle, Size = GarageSettings.small }
            };

            return vehicleTypes;
        }

        private static IEnumerable<Domain.Entities.Vehicle> GenerateVehicles(IEnumerable<Person> persons, IEnumerable<VehicleType> vehicleTypes)
        {
            var vehicles = new List<Domain.Entities.Vehicle>();
            foreach (var person in persons)
            {
                foreach(var type in vehicleTypes)
                {
                    var vehicle = new Domain.Entities.Vehicle()
                    {
                        LicenseNr = faker.Vehicle.GbRegistrationPlate(DateTime.Now, DateTime.Now),
                        Color = faker.Internet.Color(),
                        Brand = faker.Vehicle.Manufacturer(),
                        Model = faker.Vehicle.Model(),
                        Wheels = faker.Random.Int(0, 10),
                        Person = person,
                        VehicleType = type

                    };
                    vehicles.Add(vehicle);
                }
            }
            return vehicles;
        }
    }
}
