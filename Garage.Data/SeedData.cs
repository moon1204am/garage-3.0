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
//using Vehicle = Garage.Domain.Entities.Vehicle;
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

            //var persons = GeneratePersons(10);
            //await db.AddRangeAsync(persons);

            var vehicles = GenerateVehicles(20);
            await db.AddRangeAsync(vehicles);

            //var enrollments = GenerateEnrollments(courses, persons);
            //await db.AddRangeAsync(enrollments);

            await db.SaveChangesAsync();
        }

        private static IEnumerable<Person> GeneratePersons(int numberOfPersons)
        {
            var students = new List<Person>();

            for (int i = 0; i < numberOfPersons; i++)
            {
                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                var SSN = faker.Person.Personnummer();

                var person = new Person(fName, lName, SSN);

                students.Add(person);
            }

            return students;
        }

        //        LicenseNr 

        //Color 

        // Brand 

        //Model

        //    public int? Wheels

        private static IEnumerable<Domain.Entities.Vehicle> GenerateVehicles(int numberOfVehicles)
        {
            var vehicles = new List<Domain.Entities.Vehicle>();

            for (int i = 0; i < numberOfVehicles; i++)
            {
                var licenseNr = faker.Vehicle.GbRegistrationPlate(DateTime.Now, DateTime.Now);
                var color = faker.Internet.Color();
                var brand = faker.Vehicle.Manufacturer();
                var model = faker.Vehicle.Model();
                var wheels = faker.Random.Int(0, 10);

                var vehicle = new Domain.Entities.Vehicle(licenseNr, color, brand, model, wheels);
                vehicles.Add(vehicle);
            }

            return vehicles;
        }

        //private static IEnumerable<ParkingSpot> GenerateEnrollments(IEnumerable<Person> courses, IEnumerable<Vehicle> students)
        //{
        //    var rnd = new Random();

        //    var enrollments = new List<Enrollment>();

        //    foreach (var student in students)
        //    {
        //        foreach (var course in courses)
        //        {
        //            if (rnd.Next(0, 5) == 0)
        //            {
        //                var enrollment = new Enrollment
        //                {
        //                    Course = course,
        //                    Student = student,
        //                    Grade = rnd.Next(1, 6)
        //                };

        //                enrollments.Add(enrollment);
        //            }

        //        }
        //    }

        //    return enrollments;
        //}
    }
}
